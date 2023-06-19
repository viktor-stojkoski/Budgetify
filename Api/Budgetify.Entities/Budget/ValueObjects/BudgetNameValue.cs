namespace Budgetify.Entities.Budget.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class BudgetNameValue : ValueObject
{
    private const uint BudgetNameMaxLength = 255;

    public string Value { get; }

    private BudgetNameValue(string value)
    {
        Value = value;
    }

    public static Result<BudgetNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<BudgetNameValue>(ResultCodes.BudgetNameInvalid);
        }

        if (value.Length > BudgetNameMaxLength)
        {
            return Result.Invalid<BudgetNameValue>(ResultCodes.BudgetNameInvalidLength);
        }

        return Result.Ok(new BudgetNameValue(value));
    }

    public static implicit operator string(BudgetNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
