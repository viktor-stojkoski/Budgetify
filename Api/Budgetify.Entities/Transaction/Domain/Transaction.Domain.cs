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
        int categoryId,
        int currencyId,
        int? merchantId,
        string? type,
        decimal amount,
        DateTime date,
        string? description)
    {
        Result<TransactionType> typeValue = TransactionType.Create(type);

        if (typeValue.IsFailureOrNull)
        {
            return Result.FromError<Transaction>(typeValue);
        }

        if (typeValue.Value != TransactionType.Income && merchantId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionEmptyMerchantTypeInvalid);
        }

        AddDomainEvent(
            new TransactionUpdatedDomainEvent(
                UserId: UserId,
                TransactionUid: Uid,
                DifferenceAmount: Amount > amount ? -Math.Abs(Amount - amount) : Math.Abs(Amount - amount)));

        AccountId = accountId;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        MerchantId = merchantId;
        Type = typeValue.Value;
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

        AddDomainEvent(
            new TransactionDeletedDomainEvent(
                UserId: UserId,
                TransactionUid: Uid,
                DifferenceAmount: -Amount));

        return Result.Ok();
    }

    /// <summary>
    /// Adds attachment to the transaction.
    /// </summary>
    public Result<TransactionAttachment> AddTransactionAttachment(DateTime createdOn, string fileName)
    {
        string path = $"transactions/{Uid}/attachments/{fileName}";

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
}
