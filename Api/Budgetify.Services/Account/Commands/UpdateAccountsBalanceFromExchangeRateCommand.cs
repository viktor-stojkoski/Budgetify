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
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountsBalanceFromExchangeRateCommand(
    int UserId,
    Guid ExchangeRateUid,
    decimal PreviousRate) : ICommand;

public class UpdateAccountsBalanceFromExchangeRateCommandHandler
    : ICommandHandler<UpdateAccountsBalanceFromExchangeRateCommand>
{
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountsBalanceFromExchangeRateCommandHandler(
        IExchangeRateRepository exchangeRateRepository,
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _exchangeRateRepository = exchangeRateRepository;
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountsBalanceFromExchangeRateCommand command)
    {
        CommandResultBuilder result = new();

        Result<ExchangeRate> exchangeRateResult =
            await _exchangeRateRepository.GetExchangeRateAsync(command.UserId, command.ExchangeRateUid);

        if (exchangeRateResult.IsFailureOrNull)
        {
            return result.FailWith(exchangeRateResult);
        }

        Result<IEnumerable<Transaction>> transactionsResult =
            await _transactionRepository.GetTransactionsWithConversionsInDateRangeAsync(
                userId: command.UserId,
                fromDate: exchangeRateResult.Value.DateRange.FromDate,
                toDate: exchangeRateResult.Value.DateRange.ToDate);

        if (transactionsResult.IsFailureOrNull)
        {
            return result.FailWith(transactionsResult);
        }

        Result<IEnumerable<Account>> accountsResult =
            await _accountRepository.GetAccountsByIdsAsync(
                userId: command.UserId,
                accountIds: transactionsResult.Value
                    .Where(x => x.IsVerified)
                    .Select(x => x.AccountId!.Value)
                    .Distinct());

        if (accountsResult.IsFailureOrNull)
        {
            return result.FailWith(accountsResult);
        }

        foreach (Transaction transaction in transactionsResult.Value)
        {
            Account account = accountsResult.Value.Single(x => x.Id == transaction.AccountId);

            decimal previousAmount = transaction.Amount * command.PreviousRate;
            decimal newAmount = transaction.Amount * exchangeRateResult.Value.Rate;

            Result updateResult =
                account.DeductBalance(newAmount - previousAmount);

            if (updateResult.IsFailureOrNull)
            {
                return result.FailWith(updateResult);
            }

            _accountRepository.Update(account);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
