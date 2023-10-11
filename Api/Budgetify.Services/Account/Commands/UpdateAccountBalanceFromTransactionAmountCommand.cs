namespace Budgetify.Services.Account.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountBalanceFromTransactionAmountCommand(
    int UserId,
    Guid TransactionUid,
    int? PreviousAccountId,
    decimal? PreviousAmount,
    int? PreviousCurrencyId) : ICommand;

public class UpdateAccountBalanceFromTransactionAmountCommandHandler
    : ICommandHandler<UpdateAccountBalanceFromTransactionAmountCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountBalanceFromTransactionAmountCommandHandler(
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

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountBalanceFromTransactionAmountCommand command)
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

        if (command.PreviousAccountId.HasValue && command.PreviousAccountId != transactionResult.Value.AccountId)
        {
            Result previousAccountUpdateResult =
                await UpdatePreviousAccountBalance(
                    userId: command.UserId,
                    previousAccountId: command.PreviousAccountId.Value,
                    previousAccountAmount: previousAmount);

            if (previousAccountUpdateResult.IsFailureOrNull)
            {
                return result.FailWith(previousAccountUpdateResult);
            }
        }

        Result currentAccountUpdateResult =
            await UpdateCurrentAccountBalance(
                userId: command.UserId,
                transaction: transactionResult.Value,
                account: accountResult.Value,
                previousAmount: previousAmount,
                previousAccountId: command.PreviousAccountId);

        if (currentAccountUpdateResult.IsFailureOrNull)
        {
            return result.FailWith(currentAccountUpdateResult);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }

    private async Task<Result> UpdatePreviousAccountBalance(
        int userId,
        int previousAccountId,
        decimal previousAccountAmount)
    {
        Result<Account> previousAccountResult =
            await _accountRepository.GetAccountByIdAsync(userId, previousAccountId);

        if (previousAccountResult.IsFailureOrNull)
        {
            return previousAccountResult;
        }

        Result previousAccountUpdateResult =
            previousAccountResult.Value.DeductBalance(-previousAccountAmount);

        if (previousAccountUpdateResult.IsFailureOrNull)
        {
            return previousAccountUpdateResult;
        }

        _accountRepository.Update(previousAccountResult.Value);

        return Result.Ok();
    }

    private async Task<Result> UpdateCurrentAccountBalance(
        int userId,
        Transaction transaction,
        Account account,
        decimal? previousAmount,
        decimal? previousAccountId)
    {
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

        if (previousAmount.HasValue && previousAccountId == transaction.AccountId)
        {
            amount = previousAmount > amount
                ? -Math.Abs(previousAmount.Value - amount)
                : Math.Abs(previousAmount.Value - amount);
        }

        Result accountUpdateResult = account.DeductBalance(amount);

        if (accountUpdateResult.IsFailureOrNull)
        {
            return accountUpdateResult;
        }

        _accountRepository.Update(account);

        return Result.Ok();
    }
}
