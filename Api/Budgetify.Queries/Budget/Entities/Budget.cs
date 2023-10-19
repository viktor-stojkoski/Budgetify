namespace Budgetify.Queries.Budget.Entities;

using System;

using Budgetify.Queries.Category.Entities;
using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Currency.Entities;
using Budgetify.Queries.User.Entities;

public class Budget : Entity
{
    public Budget(
        int userId,
        string name,
        int categoryId,
        int currencyId,
        DateTime startDate,
        DateTime endDate,
        decimal amount,
        decimal amountSpent)
    {
        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        StartDate = startDate;
        EndDate = endDate;
        Amount = amount;
        AmountSpent = amountSpent;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public int CategoryId { get; protected internal set; }

    public int CurrencyId { get; protected internal set; }

    public DateTime StartDate { get; protected internal set; }

    public DateTime EndDate { get; protected internal set; }

    public decimal Amount { get; protected internal set; }

    public decimal AmountSpent { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Category Category { get; protected internal set; } = null!;

    public virtual Currency Currency { get; protected internal set; } = null!;
}
