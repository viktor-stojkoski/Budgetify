namespace Budgetify.Queries.Budget.Queries.GetBudgets;

using System;

public class BudgetResponse
{
    public BudgetResponse(
        Guid uid,
        string name,
        string categoryName,
        string currencyCode,
        DateTime startDate,
        DateTime endDate,
        decimal amount,
        decimal amountSpent)
    {
        Uid = uid;
        Name = name;
        CategoryName = categoryName;
        CurrencyCode = currencyCode;
        StartDate = startDate;
        EndDate = endDate;
        Amount = amount;
        AmountSpent = amountSpent;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string CategoryName { get; set; }

    public string CurrencyCode { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Amount { get; set; }

    public decimal AmountSpent { get; set; }
}
