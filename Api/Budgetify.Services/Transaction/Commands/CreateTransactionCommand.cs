namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Common.Storage;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Contracts.Settings;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Category.Domain;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateTransactionCommand(
    Guid AccountUid,
    Guid CategoryUid,
    string? CurrencyCode,
    Guid? MerchantUid,
    string? Type,
    decimal Amount,
    DateTime Date,
    string? Description,
    IEnumerable<FileForUploadRequest> Attachments) : ICommand;

public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAccountRepository _accountRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(
        ICurrentUser currentUser,
        IAccountRepository accountRepository,
        ICategoryRepository categoryRepository,
        ICurrencyRepository currencyRepository,
        IMerchantRepository merchantRepository,
        IStorageService storageService,
        IStorageSettings storageSettings,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
        _currencyRepository = currencyRepository;
        _merchantRepository = merchantRepository;
        _storageService = storageService;
        _storageSettings = storageSettings;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateTransactionCommand command)
    {
        CommandResultBuilder result = new();

        Result<Account> accountResult =
            await _accountRepository.GetAccountAsync(_currentUser.Id, command.AccountUid);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.CurrencyCode);

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        int? merchantId = null;

        if (command.MerchantUid.HasValue)
        {
            Result<Merchant> merchantResult =
                await _merchantRepository.GetMerchantAsync(_currentUser.Id, command.MerchantUid.Value);

            if (merchantResult.IsFailureOrNull)
            {
                return result.FailWith(merchantResult);
            }

            merchantId = merchantResult.Value.Id;
        }

        Result<Transaction> transactionResult =
            Transaction.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                accountId: accountResult.Value.Id,
                categoryId: categoryResult.Value.Id,
                currencyId: currencyResult.Value.Id,
                merchantId: merchantId,
                type: command.Type,
                amount: command.Amount,
                date: command.Date.ToLocalTime(),
                description: command.Description);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        if (command.Attachments.Any())
        {
            List<Task<UploadedFileResponse>> attachmentsForUploadTasks = new();

            foreach (FileForUploadRequest attachments in command.Attachments)
            {
                Result<TransactionAttachment> addTransactionAttachmentResult =
                    transactionResult.Value.UpsertTransactionAttachment(
                        createdOn: DateTime.UtcNow.ToLocalTime(),
                        fileName: attachments.Name);

                if (addTransactionAttachmentResult.IsFailureOrNull)
                {
                    return result.FailWith(addTransactionAttachmentResult);
                }

                attachmentsForUploadTasks.Add(
                    _storageService.UploadAsync(
                        containerName: _storageSettings.ContainerName,
                        fileName: addTransactionAttachmentResult.Value.FilePath,
                        content: attachments.Content,
                        contentType: attachments.Type));
            }

            await Task.WhenAll(attachmentsForUploadTasks);
        }

        _transactionRepository.Insert(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
