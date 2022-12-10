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

public record DeleteTransactionCommand(Guid TransactionUid) : ICommand;

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionCommandHandler(
        ICurrentUser currentUser,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteTransactionCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionAsync(_currentUser.Id, command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result deleteResult = transactionResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        _transactionRepository.Update(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
