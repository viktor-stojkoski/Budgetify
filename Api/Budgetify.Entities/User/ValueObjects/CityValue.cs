namespace Budgetify.Entities.User.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class CityValue : ValueObject
{
    private const uint MAX_CITY_LENGTH = 255;

    public string Value { get; }

    private CityValue(string value)
    {
        Value = value;
    }

    public static Result<CityValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<CityValue>(ResultCodes.CityInvalid);
        }

        if (value.Length > MAX_CITY_LENGTH)
        {
            return Result.Invalid<CityValue>(ResultCodes.CityInvalidLength);
        }

        return Result.Ok(new CityValue(value));
    }

    public static implicit operator string(CityValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
