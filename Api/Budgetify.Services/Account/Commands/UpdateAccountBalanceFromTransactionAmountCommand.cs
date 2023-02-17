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
    decimal DifferenceAmount) : ICommand;

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

        if (!transactionResult.Value.IsVerified
                || !transactionResult.Value.AccountId.HasValue
                    || !transactionResult.Value.Date.HasValue)
        {
            return result.FailWith(Result.Invalid(ResultCodes.TransactionNotVerifiedCannotUpdateAccountBalance));
        }

        Result<Account> accountResult =
            await _accountRepository.GetAccountByIdAsync(command.UserId, transactionResult.Value.AccountId.Value);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        decimal amount = command.DifferenceAmount;

        if (transactionResult.Value.CurrencyId != accountResult.Value.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: command.UserId,
                    fromCurrencyId: transactionResult.Value.CurrencyId,
                    toCurrencyId: accountResult.Value.CurrencyId,
                    date: transactionResult.Value.Date.Value);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return result.FailWith(exchangeRateResult);
            }

            amount *= exchangeRateResult.Value.Rate;
        }

        Result updateResult =
            accountResult.Value.DeductBalance(amount);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
