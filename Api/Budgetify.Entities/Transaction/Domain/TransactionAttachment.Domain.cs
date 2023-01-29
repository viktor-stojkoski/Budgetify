namespace Budgetify.Entities.Transaction.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.ValueObjects;

public partial class TransactionAttachment
{
    /// <summary>
    /// Updates transaction attachment.
    /// </summary>
    public Result Update(string name, string filePath)
    {
        Result<FilePathValue> filePathValue = FilePathValue.Create(filePath);
        Result<TransactionAttachmentNameValue> nameValue = TransactionAttachmentNameValue.Create(name);

        Result okOrError = Result.FirstFailureNullOrOk(filePathValue, nameValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<TransactionAttachment>(okOrError);
        }

        Name = nameValue.Value;
        FilePath = filePathValue.Value;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks transaction attachment as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        DeletedOn = deletedOn;

        MarkModify();

        return Result.Ok();
    }
}
