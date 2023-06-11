namespace Budgetify.Storage.Category.Entities;

using System;
using System.Collections.Generic;

using Budgetify.Storage.Budget.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Merchant.Entities;
using Budgetify.Storage.Transaction.Entities;
using Budgetify.Storage.User.Entities;

public class Category : AggregateRoot
{
    public Category(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        string type) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        Name = name;
        Type = type;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Type { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual ICollection<Merchant> Merchants { get; protected internal set; } = new List<Merchant>();

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();

    public virtual ICollection<Budget> Budgets { get; protected internal set; } = new List<Budget>();
}
