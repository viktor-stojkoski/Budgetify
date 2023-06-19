namespace Budgetify.Entities.Budget.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;
using Budgetify.Entities.Common.Enumerations;

public partial class Budget
{
    /// <summary>
    /// Create budget DB to domain only.
    /// </summary>
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

    /// <summary>
    /// Creates budget.
    /// </summary>
    public static Result<Budget> Create(
        DateTime createdOn,
        int userId,
        string? name,
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
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
