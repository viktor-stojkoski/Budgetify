namespace Budgetify.Services.Account.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Entities.Transaction.Enumerations;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountsBalanceFromTransactionCommand(
    int UserId,
    Guid TransactionUid,
    int? PreviousAccountId,
    int? PreviousFromAccountId,
    decimal? PreviousAmount,
    int? PreviousCurrencyId) : ICommand;

public class UpdateAccountsBalanceFromTransactionCommandHandler
    : ICommandHandler<UpdateAccountsBalanceFromTransactionCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountsBalanceFromTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountsBalanceFromTransactionCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionAsync(command.UserId, command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        if (!transactionResult.Value.AccountId.HasValue || !transactionResult.Value.Date.HasValue)
        {
            return result.FailWith(Result.Invalid(ResultCodes.TransactionNotVerifiedCannotUpdateAccountBalance));
        }

        Result<Account> accountResult =
            await _accountRepository.GetAccountByIdAsync(command.UserId, transactionResult.Value.AccountId.Value);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        decimal previousAmount = command.PreviousAmount ?? transactionResult.Value.Amount;

        if (command.PreviousCurrencyId.HasValue && accountResult.Value.CurrencyId != command.PreviousCurrencyId)
        {
            Result<ExchangeRate> previousAmountExchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: command.UserId,
                    fromCurrencyId: command.PreviousCurrencyId.Value,
                    toCurrencyId: accountResult.Value.CurrencyId,
                    date: transactionResult.Value.Date.Value);

            if (previousAmountExchangeRateResult.IsFailureOrNull)
            {
                return result.FailWith(previousAmountExchangeRateResult);
            }

            previousAmount *= previousAmountExchangeRateResult.Value.Rate;
        }

        if (transactionResult.Value.Type == TransactionType.Expense)
        {
            Result updateFromExpenseResult = await UpdateFromExpenseTransaction(
                userId: command.UserId,
                transaction: transactionResult.Value,
                previousAmount: previousAmount,
                previousAccountId: command.PreviousAccountId,
                account: accountResult.Value);

            if (updateFromExpenseResult.IsFailureOrNull)
            {
                return result.FailWith(updateFromExpenseResult);
            }
        }

        if (transactionResult.Value.Type == TransactionType.Income)
        {
            Result updateFromIncomeResult = await UpdateFromIncomeTransaction(
                userId: command.UserId,
                transaction: transactionResult.Value,
                previousAmount: previousAmount,
                previousAccountId: command.PreviousAccountId,
                account: accountResult.Value);

            if (updateFromIncomeResult.IsFailureOrNull)
            {
                return result.FailWith(updateFromIncomeResult);
            }
        }

        if (transactionResult.Value.Type == TransactionType.Transfer)
        {
            Result updateFromTransferResult = await UpdateFromTransferTransaction(
                userId: command.UserId,
                transaction: transactionResult.Value,
                previousAmount: previousAmount,
                previousAccountId: command.PreviousAccountId,
                previousFromAccountId: command.PreviousFromAccountId);

            if (updateFromTransferResult.IsFailureOrNull)
            {
                return result.FailWith(updateFromTransferResult);
            }
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }

    private async Task<Result> UpdateFromExpenseTransaction(
        int userId,
        Transaction transaction,
        decimal previousAmount,
        int? previousAccountId,
        Account account)
    {
        // Update previous account (changed one)
        if (previousAccountId.HasValue && previousAccountId != transaction.AccountId)
        {
            Result<Account> previousAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, previousAccountId.Value);

            if (previousAccountResult.IsFailureOrNull)
            {
                return previousAccountResult;
            }

            Result previousAccountUpdateResult =
                previousAccountResult.Value.DeductBalance(-previousAmount);

            if (previousAccountUpdateResult.IsFailureOrNull)
            {
                return previousAccountUpdateResult;
            }

            _accountRepository.Update(previousAccountResult.Value);
        }

        // Update current account
        decimal amount = transaction.Amount;

        if (transaction.CurrencyId != account.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: userId,
                    fromCurrencyId: transaction.CurrencyId,
                    toCurrencyId: account.CurrencyId,
                    date: transaction.Date!.Value);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return exchangeRateResult;
            }

            amount *= exchangeRateResult.Value.Rate;
        }

        if (previousAccountId == transaction.AccountId)
        {
            amount = previousAmount > amount
                ? -Math.Abs(previousAmount - amount)
                : Math.Abs(previousAmount - amount);
        }

        Result accountUpdateResult = account.DeductBalance(amount);

        if (accountUpdateResult.IsFailureOrNull)
        {
            return accountUpdateResult;
        }

        _accountRepository.Update(account);

        return Result.Ok();
    }

    private async Task<Result> UpdateFromIncomeTransaction(
        int userId,
        Transaction transaction,
        decimal previousAmount,
        int? previousAccountId,
        Account account)
    {
        // Update previous account (changed one)
        if (previousAccountId.HasValue && previousAccountId != transaction.AccountId)
        {
            Result<Account> previousAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, previousAccountId.Value);

            if (previousAccountResult.IsFailureOrNull)
            {
                return previousAccountResult;
            }

            Result previousAccountUpdateResult =
                previousAccountResult.Value.DeductBalance(previousAmount);

            if (previousAccountUpdateResult.IsFailureOrNull)
            {
                return previousAccountUpdateResult;
            }

            _accountRepository.Update(previousAccountResult.Value);
        }

        // Update current account
        decimal amount = transaction.Amount;

        if (transaction.CurrencyId != account.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: userId,
                    fromCurrencyId: transaction.CurrencyId,
                    toCurrencyId: account.CurrencyId,
                    date: transaction.Date!.Value);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return exchangeRateResult;
            }

            amount *= exchangeRateResult.Value.Rate;
        }

        if (previousAccountId == transaction.AccountId)
        {
            amount = previousAmount > amount
                ? -Math.Abs(previousAmount - amount)
                : Math.Abs(previousAmount - amount);
        }

        Result accountUpdateResult = account.DeductBalance(-amount);

        if (accountUpdateResult.IsFailureOrNull)
        {
            return accountUpdateResult;
        }

        _accountRepository.Update(account);

        return Result.Ok();
    }

    private async Task<Result> UpdateFromTransferTransaction(
        int userId,
        Transaction transaction,
        decimal previousAmount,
        int? previousAccountId,
        int? previousFromAccountId)
    {
        List<Account> accounts = new();

        // Update previous from account (changed one)
        if (previousFromAccountId.HasValue && previousFromAccountId != transaction.FromAccountId)
        {
            Result<Account> previousFromAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, previousFromAccountId.Value);

            if (previousFromAccountResult.IsFailureOrNull)
            {
                return previousFromAccountResult;
            }

            Result previousFromAccountUpdateResult =
                previousFromAccountResult.Value.DeductBalance(-previousAmount);

            if (previousFromAccountUpdateResult.IsFailureOrNull)
            {
                return previousFromAccountUpdateResult;
            }

            _accountRepository.Update(previousFromAccountResult.Value);

            accounts.Add(previousFromAccountResult.Value);
        }

        // Update previous account (changed one)
        if (previousAccountId.HasValue && previousAccountId != transaction.AccountId)
        {
            Result<Account> previousAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, previousAccountId.Value);

            if (previousAccountResult.IsFailureOrNull)
            {
                return previousAccountResult;
            }

            Result previousAccountUpdateResult =
                previousAccountResult.Value.DeductBalance(previousAmount);

            if (previousAccountUpdateResult.IsFailureOrNull)
            {
                return previousAccountUpdateResult;
            }

            _accountRepository.Update(previousAccountResult.Value);

            accounts.Add(previousAccountResult.Value);
        }

        // Update current from account
        Account? fromAccount = null;

        if (accounts.Any(x => x.Id == transaction.FromAccountId))
        {
            fromAccount = accounts.Single(x => x.Id == transaction.FromAccountId);
        }
        else
        {
            Result<Account> fromAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, transaction.FromAccountId!.Value);

            if (fromAccountResult.IsFailureOrNull)
            {
                return fromAccountResult;
            }

            fromAccount = fromAccountResult.Value;
        }

        decimal fromAmount = transaction.Amount;

        if (transaction.CurrencyId != fromAccount.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: userId,
                    fromCurrencyId: transaction.CurrencyId,
                    toCurrencyId: fromAccount.CurrencyId,
                    date: transaction.Date!.Value);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return exchangeRateResult;
            }

            fromAmount *= exchangeRateResult.Value.Rate;
        }

        if (previousFromAccountId == transaction.FromAccountId)
        {
            fromAmount = previousAmount > fromAmount
                ? -Math.Abs(previousAmount - fromAmount)
                : Math.Abs(previousAmount - fromAmount);
        }

        Result accountUpdateResult = fromAccount.DeductBalance(fromAmount);

        if (accountUpdateResult.IsFailureOrNull)
        {
            return accountUpdateResult;
        }

        _accountRepository.Update(fromAccount);

        // Update current ToAccount
        Account? toAccount = null;

        if (accounts.Any(x => x.Id == transaction.AccountId))
        {
            toAccount = accounts.Single(x => x.Id == transaction.AccountId);
        }
        else
        {
            Result<Account> toAccountResult =
                await _accountRepository.GetAccountByIdAsync(userId, transaction.AccountId!.Value);

            if (toAccountResult.IsFailureOrNull)
            {
                return toAccountResult;
            }

            toAccount = toAccountResult.Value;
        }

        decimal toAmount = transaction.Amount;

        if (transaction.CurrencyId != toAccount.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: userId,
                    fromCurrencyId: transaction.CurrencyId,
                    toCurrencyId: toAccount.CurrencyId,
                    date: transaction.Date!.Value);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return exchangeRateResult;
            }

            toAmount *= exchangeRateResult.Value.Rate;
        }

        if (previousAccountId == transaction.AccountId)
        {
            toAmount = previousAmount > toAmount
                ? -Math.Abs(previousAmount - toAmount)
                : Math.Abs(previousAmount - toAmount);
        }

        Result toAccountUpdateResult = toAccount.DeductBalance(-toAmount);

        if (toAccountUpdateResult.IsFailureOrNull)
        {
            return toAccountUpdateResult;
        }

        _accountRepository.Update(toAccount);

        return Result.Ok();
    }
}
