namespace Budgetify.Storage.Budget.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.Domain;

internal static class BudgetFactory
{
    /// <summary>
    /// Creates <see cref="Budget"/> domain entity for a given <see cref="Entities.Budget"/> storage entity.
    /// </summary>
    internal static Result<Budget> CreateBudget(this Entities.Budget dbBudget)
    {
        return Budget.Create(
            id: dbBudget.Id,
            uid: dbBudget.Uid,
            createdOn: dbBudget.CreatedOn,
            deletedOn: dbBudget.DeletedOn,
            userId: dbBudget.UserId,
            name: dbBudget.Name,
            categoryId: dbBudget.CategoryId,
            currencyId: dbBudget.CurrencyId,
            startDate: dbBudget.StartDate,
            endDate: dbBudget.EndDate,
            amount: dbBudget.Amount,
            amountSpent: dbBudget.AmountSpent);
    }

    /// <summary>
    /// Creates <see cref="Entities.Budget"/> storage entity for a given <see cref="Budget"/> domain entity.
    /// </summary>
    internal static Entities.Budget CreateBudget(this Budget budget)
    {
        return new(
            id: budget.Id,
            uid: budget.Uid,
            createdOn: budget.CreatedOn,
            deletedOn: budget.DeletedOn,
            userId: budget.UserId,
            name: budget.Name,
            categoryId: budget.CategoryId,
            currencyId: budget.CurrencyId,
            startDate: budget.DateRange.StartDate,
            endDate: budget.DateRange.EndDate,
            amount: budget.Amount,
            amountSpent: budget.AmountSpent);
    }
}
