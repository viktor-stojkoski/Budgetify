namespace Budgetify.Entities.Budget.Domain;

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
}
