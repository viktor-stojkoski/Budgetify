namespace Budgetify.Entities.Transaction.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class FilePathValue : ValueObject
{
    public string Value { get; }

    private FilePathValue(string value)
    {
        Value = value;
    }

    public static Result<FilePathValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<FilePathValue>(ResultCodes.TransactionAttachmentFilePathInvalid);
        }

        return Result.Ok(new FilePathValue(value));
    }

    public static implicit operator string(FilePathValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
