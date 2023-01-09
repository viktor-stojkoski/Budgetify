namespace Budgetify.Entities.Transaction.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.ValueObjects;

public partial class TransactionAttachment
{
    /// <summary>
    /// Create transaction attachment DB to domain only.
    /// </summary>
    public static Result<TransactionAttachment> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int transactionId,
        string filePath,
        string name)
    {
        Result<FilePathValue> filePathValue = FilePathValue.Create(filePath);
        Result<TransactionAttachmentNameValue> nameValue = TransactionAttachmentNameValue.Create(name);

        Result okOrError = Result.FirstFailureNullOrOk(filePathValue, nameValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<TransactionAttachment>(okOrError);
        }

        return Result.Ok(
            new TransactionAttachment(
                transactionId: transactionId,
                filePath: filePathValue.Value,
                name: nameValue.Value)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates transaction attachment.
    /// </summary>
    public static Result<TransactionAttachment> Create(
        DateTime createdOn,
        int transactionId,
        string filePath,
        string name)
    {
        Result<FilePathValue> filePathValue = FilePathValue.Create(filePath);
        Result<TransactionAttachmentNameValue> nameValue = TransactionAttachmentNameValue.Create(name);

        Result okOrError = Result.FirstFailureNullOrOk(filePathValue, nameValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<TransactionAttachment>(okOrError);
        }

        return Result.Ok(
            new TransactionAttachment(
                transactionId: transactionId,
                filePath: filePathValue.Value,
                name: nameValue.Value)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
