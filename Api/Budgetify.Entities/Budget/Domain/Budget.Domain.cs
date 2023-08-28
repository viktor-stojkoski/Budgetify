namespace Budgetify.Entities.Budget.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;

public partial class Budget
{
    /// <summary>
    /// Updates budget.
    /// </summary>
    public Result Update(string? name, decimal amount)
    {
        Result<BudgetNameValue> nameValue = BudgetNameValue.Create(name);

        if (nameValue.IsFailureOrNull)
        {
            return Result.FromError<Budget>(nameValue);
        }

        Name = nameValue.Value;
        Amount = amount;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks budget as deleted.
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

    /// <summary>
    /// Deducts the given amount from amount spent.
    /// </summary>
    public Result UpdateAmountSpent(decimal amount)
    {
        AmountSpent += amount;

        MarkModify();

        return Result.Ok();
    }
}
