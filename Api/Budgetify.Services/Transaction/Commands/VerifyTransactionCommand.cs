namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record VerifyTransactionCommand(Guid TransactionUid) : ICommand;

public class VerifyTransactionCommandHandler : ICommandHandler<VerifyTransactionCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyTransactionCommandHandler(
        ICurrentUser currentUser,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(VerifyTransactionCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionAsync(_currentUser.Id, command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result verifiedResult = transactionResult.Value.Verify();

        if (verifiedResult.IsFailureOrNull)
        {
            return result.FailWith(verifiedResult);
        }

        _transactionRepository.Update(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
