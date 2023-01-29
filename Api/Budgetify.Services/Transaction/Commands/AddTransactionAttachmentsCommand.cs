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

public record AddTransactionAttachmentsCommand(
    Guid TransactionUid,
    IEnumerable<FileForUploadRequest> Attachments) : ICommand;

public class AddTransactionAttachmentsCommandHandler : ICommandHandler<AddTransactionAttachmentsCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;
    private readonly IUnitOfWork _unitOfWork;

    public AddTransactionAttachmentsCommandHandler(
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

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(AddTransactionAttachmentsCommand command)
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

        if (command.Attachments.Any())
        {
            List<Task<UploadedFileResponse>> attachmentsForUploadTasks = new();

            foreach (FileForUploadRequest attachment in command.Attachments)
            {
                Result<TransactionAttachment> upsertTransactionAttachmentResult =
                    transactionResult.Value.UpsertTransactionAttachment(
                        createdOn: DateTime.UtcNow.ToLocalTime(),
                        fileName: attachment.Name);

                if (upsertTransactionAttachmentResult.IsFailureOrNull)
                {
                    return result.FailWith(upsertTransactionAttachmentResult);
                }

                attachmentsForUploadTasks.Add(
                    _storageService.UploadAsync(
                        containerName: _storageSettings.ContainerName,
                        fileName: upsertTransactionAttachmentResult.Value.FilePath,
                        content: attachment.Content,
                        contentType: attachment.Type));
            }

            await Task.WhenAll(attachmentsForUploadTasks);

            _transactionRepository.Update(transactionResult.Value);

            await _unitOfWork.SaveAsync();
        }

        return result.Build();
    }
}
