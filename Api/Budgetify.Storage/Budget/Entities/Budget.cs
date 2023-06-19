namespace Budgetify.Storage.Budget.Entities;

using System;

using Budgetify.Storage.Category.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.User.Entities;

public class Budget : AggregateRoot
{
    public Budget(
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
        decimal amountSpent) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        Name = name;
        CategoryId = categoryId;
        StartDate = startDate;
        EndDate = endDate;
        Amount = amount;
        AmountSpent = amountSpent;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public int CategoryId { get; protected internal set; }

    public DateTime StartDate { get; protected internal set; }

    public DateTime EndDate { get; protected internal set; }

    public decimal Amount { get; protected internal set; }

    public decimal AmountSpent { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Category Category { get; protected internal set; } = null!;
}
