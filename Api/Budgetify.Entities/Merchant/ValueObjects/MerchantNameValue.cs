namespace Budgetify.Entities.Merchant.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class MerchantNameValue : ValueObject
{
    private const uint MerchantNameMaxLength = 255;

    public string Value { get; }

    private MerchantNameValue(string value)
    {
        Value = value;
    }

    public static Result<MerchantNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<MerchantNameValue>(ResultCodes.MerchantNameInvalid);
        }

        if (value.Length > MerchantNameMaxLength)
        {
            return Result.Invalid<MerchantNameValue>(ResultCodes.MerchantNameInvalidLength);
        }

        return Result.Ok(new MerchantNameValue(value));
    }

    public static implicit operator string(MerchantNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
