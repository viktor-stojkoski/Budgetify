namespace Budgetify.Entities.Currency.ValueObjects;

using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class CurrencyCodeValue : ValueObject
{
    private const uint CurrencyCodeLength = 3;

    public string Value { get; }

    private CurrencyCodeValue(string value)
    {
        Value = value;
    }

    public static Result<CurrencyCodeValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<CurrencyCodeValue>(ResultCodes.CurrencyCodeInvalid);
        }

        if (value.Length != CurrencyCodeLength || !value.All(char.IsLetter) || !value.All(char.IsUpper))
        {
            return Result.Invalid<CurrencyCodeValue>(ResultCodes.CurrencyCodeInvalid);
        }

        return Result.Ok(new CurrencyCodeValue(value));
    }

    public static implicit operator string(CurrencyCodeValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
