namespace Budgetify.Entities.Budget.Domain;

using Budgetify.Entities.Budget.ValueObjects;
using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;

public sealed partial class Budget : AggregateRoot
{
    public Budget(
        int userId,
        BudgetNameValue name,
        int categoryId,
        BudgetDateRangeValue dateRange,
        decimal amount,
        decimal amountSpent)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        DateRange = dateRange;
        Amount = amount;
        AmountSpent = amountSpent;
    }

    /// <summary>
    /// User that owns this budget.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Budget's name.
    /// </summary>
    public BudgetNameValue Name { get; private set; }

    /// <summary>
    /// Budget's category.
    /// </summary>
    public int CategoryId { get; private set; }

    /// <summary>
    /// Budget's date range.
    /// </summary>
    public BudgetDateRangeValue DateRange { get; private set; }

    /// <summary>
    /// Budget's amount.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// Budget's amount spent.
    /// </summary>
    public decimal AmountSpent { get; private set; }
}
