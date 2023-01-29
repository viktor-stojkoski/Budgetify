namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
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

public record DeleteTransactionCommand(Guid TransactionUid) : ICommand;

public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionCommandHandler(
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

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteTransactionCommand command)
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

        Result deleteResult = transactionResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        if (transactionResult.Value.Attachments.Any())
        {
            await DeleteAttachmentsFromStorageAsync(
                transactionResult.Value.Attachments
                    .Select(x => x.FilePath)
                        .Select(x => x.Value));
        }

        _transactionRepository.Update(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }

    private async Task DeleteAttachmentsFromStorageAsync(IEnumerable<string> attachmentPaths)
    {
        List<Task> deleteAttachmentsTasks = new();

        foreach (string attachmentPath in attachmentPaths)
        {
            deleteAttachmentsTasks.Add(
                _storageService.DeleteFileAsync(
                    _storageSettings.ContainerName,
                    attachmentPath));
        }

        await Task.WhenAll(deleteAttachmentsTasks);
    }
}
