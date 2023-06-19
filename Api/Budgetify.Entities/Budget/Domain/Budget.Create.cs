namespace Budgetify.Entities.Budget.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;

public partial class Budget
{
    public static Result<Budget> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        int categoryId,
        DateTime startDate,
        DateTime endDate,
        decimal amount,
        decimal amountSpent)
    {
        Result<BudgetNameValue> nameValue = BudgetNameValue.Create(name);

        if (nameValue.IsFailureOrNull)
        {
            return Result.FromError<Budget>(nameValue);
        }

        return Result.Ok(
            new Budget(
                userId: userId,
                name: nameValue.Value,
                categoryId: categoryId,
                startDate: startDate,
                endDate: endDate,
                amount: amount,
                amountSpent: amountSpent)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }
}
