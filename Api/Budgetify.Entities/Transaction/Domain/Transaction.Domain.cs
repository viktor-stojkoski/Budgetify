namespace Budgetify.Entities.Transaction.Domain;

using System;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Entities.Transaction.Enumerations;

public partial class Transaction
{
    /// <summary>
    /// Updates transaction.
    /// </summary>
    public Result Update(
        int accountId,
        int? fromAccountId,
        int? categoryId,
        int currencyId,
        int? merchantId,
        decimal amount,
        DateTime date,
        string? description)
    {
        if (Type != TransactionType.Income && merchantId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionEmptyMerchantTypeInvalid);
        }

        if (Type != TransactionType.Expense && merchantId is not null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionTypeNotCompatibleWithMerchant);
        }

        if (Type != TransactionType.Transfer && categoryId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionCategoryMissing);
        }

        if (IsVerified)
        {
            AddDomainEvent(
                new TransactionUpdatedDomainEvent(
                    UserId: UserId,
                    TransactionUid: Uid,
                    TransactionType: Type,
                    PreviousAccountId: AccountId,
                    PreviousAmount: Amount,
                    PreviousCurrencyId: CurrencyId,
                    PreviousCategoryId: CategoryId));
        }

        AccountId = accountId;
        FromAccountId = accountId;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        MerchantId = merchantId;
        Amount = amount;
        Date = date;
        Description = description;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks transaction as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        Result deleteAttachmentsResult = DeleteTransactionAttachments(deletedOn);

        if (deleteAttachmentsResult.IsFailureOrNull)
        {
            return deleteAttachmentsResult;
        }

        DeletedOn = deletedOn;

        MarkModify();

        if (IsVerified)
        {
            AddDomainEvent(
                new TransactionDeletedDomainEvent(
                    UserId: UserId,
                    AccountId: AccountId!.Value,
                    CurrencyId: CurrencyId,
                    CategoryId: CategoryId!.Value,
                    Amount: -Amount,
                    Date: Date!.Value,
                    TransactionType: Type));
        }

        return Result.Ok();
    }

    /// <summary>
    /// Verifies transaction
    /// </summary>
    public Result Verify()
    {
        if (IsVerified)
        {
            return Result.Ok();
        }

        if (!AccountId.HasValue || !CategoryId.HasValue || !Date.HasValue)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionInvalidForVerification);
        }

        if (Type != TransactionType.Income && MerchantId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionEmptyMerchantTypeInvalid);
        }

        if (Type != TransactionType.Expense && MerchantId is not null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionTypeNotCompatibleWithMerchant);
        }

        if (Type != TransactionType.Transfer && CategoryId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionCategoryMissing);
        }

        IsVerified = true;

        MarkModify();

        AddDomainEvent(
            new TransactionCreatedDomainEvent(
                UserId: UserId,
                TransactionUid: Uid,
                TransactionType: Type));

        return Result.Ok();
    }

    /// <summary>
    /// Adds or updates transaction attachment.
    /// </summary>
    public Result<TransactionAttachment> UpsertTransactionAttachment(DateTime createdOn, string fileName)
    {
        string path = $"transactions/{Uid}/attachments/{fileName}";

        Result<TransactionAttachment> existingAttachmentResult = GetTransactionAttachmentByName(fileName);

        if (existingAttachmentResult.IsNotFound)
        {
            Result<TransactionAttachment> transactionAttachmentResult =
                TransactionAttachment.Create(
                    createdOn: createdOn,
                    transactionId: Id,
                    filePath: path,
                    name: fileName);

            if (transactionAttachmentResult.IsFailureOrNull)
            {
                return transactionAttachmentResult;
            }

            _attachments.Add(transactionAttachmentResult.Value);

            return Result.Ok(transactionAttachmentResult.Value);
        }

        Result updateResult =
            existingAttachmentResult.Value.Update(fileName, path);

        if (updateResult.IsFailureOrNull)
        {
            return Result.FromError<TransactionAttachment>(updateResult);
        }

        return Result.Ok(existingAttachmentResult.Value);
    }

    /// <summary>
    /// Deletes transaction attachment.
    /// </summary>
    public Result<TransactionAttachment> DeleteTransactionAttachment(Guid attachmentUid, DateTime deletedOn)
    {
        Result<TransactionAttachment> attachmentResult = GetTransactionAttachment(attachmentUid);

        if (attachmentResult.IsFailureOrNull)
        {
            return attachmentResult;
        }

        Result deleteResult = attachmentResult.Value.Delete(deletedOn);

        if (deleteResult.IsFailureOrNull)
        {
            return Result.FromError<TransactionAttachment>(deleteResult);
        }

        return Result.Ok(attachmentResult.Value);
    }

    /// <summary>
    /// Deletes all the transaction's attachments.
    /// </summary>
    public Result DeleteTransactionAttachments(DateTime deletedOn)
    {
        foreach (TransactionAttachment transactionAttachment in _attachments)
        {
            Result deleteResult = transactionAttachment.Delete(deletedOn);

            if (deleteResult.IsFailureOrNull)
            {
                return deleteResult;
            }
        }

        return Result.Ok();
    }

    /// <summary>
    /// Returns transaction attachment by given attachmentUid.
    /// </summary>
    private Result<TransactionAttachment> GetTransactionAttachment(Guid attachmentUid)
    {
        TransactionAttachment? transactionAttachment =
            _attachments.SingleOrDefault(x => x.DeletedOn == null && x.Uid == attachmentUid);

        if (transactionAttachment is null)
        {
            return Result.NotFound<TransactionAttachment>(ResultCodes.TransactionAttachmentNotFound);
        }

        return Result.Ok(transactionAttachment);
    }

    /// <summary>
    /// Returns transaction attachment by given fileName.
    /// </summary>
    private Result<TransactionAttachment> GetTransactionAttachmentByName(string name)
    {
        TransactionAttachment? transactionAttachment =
            _attachments.SingleOrDefault(x => x.DeletedOn == null && x.Name == name);

        if (transactionAttachment is null)
        {
            return Result.NotFound<TransactionAttachment>(ResultCodes.TransactionAttachmentNotFound);
        }

        return Result.Ok(transactionAttachment);
    }
}
