namespace Budgetify.Entities.Category.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class CategoryNameValue : ValueObject
{
    private const uint CategoryNameMaxLength = 255;

    public string Value { get; }

    private CategoryNameValue(string value)
    {
        Value = value;
    }

    public static Result<CategoryNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<CategoryNameValue>(ResultCodes.CategoryNameInvalid);
        }

        if (value.Length > CategoryNameMaxLength)
        {
            return Result.Invalid<CategoryNameValue>(ResultCodes.CategoryNameInvalidLength);
        }

        return Result.Ok(new CategoryNameValue(value));
    }

    public static implicit operator string(CategoryNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
