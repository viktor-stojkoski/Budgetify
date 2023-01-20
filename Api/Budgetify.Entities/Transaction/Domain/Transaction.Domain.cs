namespace Budgetify.Entities.Transaction.Domain;

using System;

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

        MarkModify();

        return Result.Ok(transactionAttachmentResult.Value);
    }
}
