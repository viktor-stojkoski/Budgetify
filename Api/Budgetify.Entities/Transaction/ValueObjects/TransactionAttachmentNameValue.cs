namespace Budgetify.Entities.Transaction.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class TransactionAttachmentNameValue : ValueObject
{
    private const uint TransactionAttachmentNameMaxLength = 255;

    public string Value { get; }

    private TransactionAttachmentNameValue(string value)
    {
        Value = value;
    }

    public static Result<TransactionAttachmentNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<TransactionAttachmentNameValue>(ResultCodes.TransactionAttachmentNameInvalid);
        }

        if (value.Length > TransactionAttachmentNameMaxLength)
        {
            return Result.Invalid<TransactionAttachmentNameValue>(ResultCodes.TransactionAttachmentNameInvalidLength);
        }

        return Result.Ok(new TransactionAttachmentNameValue(value));
    }

    public static implicit operator string(TransactionAttachmentNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
