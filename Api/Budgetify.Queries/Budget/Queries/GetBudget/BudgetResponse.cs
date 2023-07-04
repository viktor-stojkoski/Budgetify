namespace Budgetify.Queries.Budget.Queries.GetBudget;

using System;

public class BudgetResponse
{
    public BudgetResponse(
        string name,
        string categoryName,
        DateTime startDate,
        DateTime endDate,
        decimal amount,
        decimal amountSpent)
    {
        Name = name;
        CategoryName = categoryName;
        StartDate = startDate;
        EndDate = endDate;
        Amount = amount;
        AmountSpent = amountSpent;
    }

    public string Name { get; set; }

    public string CategoryName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Amount { get; set; }

    public decimal AmountSpent { get; set; }
}
