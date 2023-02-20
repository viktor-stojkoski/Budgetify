namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Common.ScanReceipt;
using Budgetify.Common.Storage;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Contracts.Settings;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateTransactionByScanCommand(FileForUploadRequest Attachment) : ICommand;

public class CreateTransactionByScanCommandHandler : ICommandHandler<CreateTransactionByScanCommand>
{
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;
    private readonly IScanReceiptService _scanReceiptService;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionByScanCommandHandler(
        IStorageService storageService,
        IStorageSettings storageSettings,
        IScanReceiptService scanReceiptService,
        ICurrencyRepository currencyRepository,
        IMerchantRepository merchantRepository,
        ICurrentUser currentUser,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _storageService = storageService;
        _storageSettings = storageSettings;
        _scanReceiptService = scanReceiptService;
        _currencyRepository = currencyRepository;
        _merchantRepository = merchantRepository;
        _currentUser = currentUser;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateTransactionByScanCommand command)
    {
        CommandResultBuilder result = new();

        SignedUrlResponse signedUrlResponse =
            await _storageService.UploadAndGetSignedUrlAsync(
                containerName: _storageSettings.ContainerName,
                fileName: $"attachments-for-scan/{Guid.NewGuid()}",
                content: command.Attachment.Content,
                contentType: command.Attachment.Type,
                expiresOn: DateTime.UtcNow.AddHours(1));

        ScanReceiptResponse scanReceiptResult =
            await _scanReceiptService.ScanReceiptAsync(signedUrlResponse.Url);

        await _storageService.DeleteFileAsync(_storageSettings.ContainerName, signedUrlResponse.FileName);

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync("MKD");

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        int? merchantId = null;

        if (scanReceiptResult.AttachmentFields?.MerchantName is not null)
        {
            Result<Merchant> merchantResult =
                await _merchantRepository.GetMerchantByNameAsync(
                    userId: _currentUser.Id,
                    name: scanReceiptResult.AttachmentFields.MerchantName);

            if (merchantResult.IsSuccess)
            {
                merchantId = merchantResult.Value.Id;
            }
        }

        Result<Transaction> transactionResult =
            Transaction.CreateByScan(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                merchantId: merchantId,
                currencyId: currencyResult.Value.Id,
                amount: scanReceiptResult.AttachmentFields?.TotalAmount ?? decimal.Zero,
                date: scanReceiptResult.AttachmentFields?.Date);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result<TransactionAttachment> upsertTransactionAttachmentResult =
            transactionResult.Value.UpsertTransactionAttachment(
                createdOn: DateTime.UtcNow.ToLocalTime(),
                fileName: command.Attachment.Name);

        if (upsertTransactionAttachmentResult.IsFailureOrNull)
        {
            return result.FailWith(upsertTransactionAttachmentResult);
        }

        await _storageService.UploadAsync(
            containerName: _storageSettings.ContainerName,
            fileName: upsertTransactionAttachmentResult.Value.FilePath,
            content: command.Attachment.Content,
            contentType: command.Attachment.Type);

        _transactionRepository.Insert(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
