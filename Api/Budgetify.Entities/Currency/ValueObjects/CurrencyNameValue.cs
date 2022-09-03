namespace Budgetify.Entities.Currency.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class CurrencyNameValue : ValueObject
{
    private const uint CurrencyNameMaxLength = 50;

    public string Value { get; }

    private CurrencyNameValue(string value)
    {
        Value = value;
    }

    public static Result<CurrencyNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<CurrencyNameValue>(ResultCodes.CurrencyNameInvalid);
        }

        if (value.Length > CurrencyNameMaxLength)
        {
            return Result.Invalid<CurrencyNameValue>(ResultCodes.CurrencyNameInvalidLength);
        }

        return Result.Ok(new CurrencyNameValue(value));
    }

    public static implicit operator string(CurrencyNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
