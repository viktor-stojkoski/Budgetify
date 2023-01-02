namespace Budgetify.Services.Account.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountBalanceFromTransactionAmountCommand(
    Guid TransactionUid,
    decimal? DifferenceAmount) : ICommand;

public class UpdateAccountBalanceFromTransactionAmountCommandHandler
    : ICommandHandler<UpdateAccountBalanceFromTransactionAmountCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountBalanceFromTransactionAmountCommandHandler(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountBalanceFromTransactionAmountCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionByUidAsync(command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result<Account> accountResult =
            await _accountRepository.GetAccountByIdAsync(transactionResult.Value.AccountId);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result updateResult =
            accountResult.Value.DeductBalance(command.DifferenceAmount ?? transactionResult.Value.Amount);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
