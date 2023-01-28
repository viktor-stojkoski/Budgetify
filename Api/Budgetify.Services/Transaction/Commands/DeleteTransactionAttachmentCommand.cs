namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Common.Storage;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Settings;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record DeleteTransactionAttachmentCommand(Guid TransactionUid, Guid AttachmentUid) : ICommand;

public class DeleteTransactionAttachmentCommandHandler : ICommandHandler<DeleteTransactionAttachmentCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionAttachmentCommandHandler(
        ICurrentUser currentUser,
        ITransactionRepository transactionRepository,
        IStorageService storageService,
        IStorageSettings storageSettings,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _transactionRepository = transactionRepository;
        _storageService = storageService;
        _storageSettings = storageSettings;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteTransactionAttachmentCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionWithAttachmentsAsync(
                userId: _currentUser.Id,
                transactionUid: command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result<TransactionAttachment> deleteTransactionAttachmentResult =
            transactionResult.Value.DeleteTransactionAttachment(
                attachmentUid: command.AttachmentUid,
                deletedOn: DateTime.UtcNow.ToLocalTime());

        if (deleteTransactionAttachmentResult.IsFailureOrNull)
        {
            return result.FailWith(deleteTransactionAttachmentResult);
        }

        await _storageService.DeleteFileAsync(
            containerName: _storageSettings.ContainerName,
            fileName: deleteTransactionAttachmentResult.Value.FilePath);

        _transactionRepository.Update(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
