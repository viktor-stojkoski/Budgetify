namespace Budgetify.Entities.Currency.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class CurrencySymbolValue : ValueObject
{
    private const uint CurrencySymbolMaxLength = 10;

    public string? Value { get; }

    private CurrencySymbolValue(string? value)
    {
        Value = value;
    }

    public static Result<CurrencySymbolValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Ok(new CurrencySymbolValue(value));
        }

        if (value.Length > CurrencySymbolMaxLength)
        {
            return Result.Invalid<CurrencySymbolValue>(ResultCodes.CurrencySymbolInvalidLength);
        }

        return Result.Ok(new CurrencySymbolValue(value));
    }

    public static implicit operator string?(CurrencySymbolValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (Value is null)
        {
            yield break;
        }

        yield return Value;
    }
}
