namespace Budgetify.Storage.Account.Entities;

using System;
using System.Collections.Generic;

using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Currency.Entities;
using Budgetify.Storage.Transaction.Entities;
using Budgetify.Storage.User.Entities;

public class Account : AggregateRoot
{
    public Account(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        string type,
        decimal balance,
        int currencyId,
        string? description) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        Name = name;
        Type = type;
        Balance = balance;
        CurrencyId = currencyId;
        Description = description;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Type { get; protected internal set; }

    public decimal Balance { get; protected internal set; }

    public int CurrencyId { get; protected internal set; }

    public string? Description { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Currency Currency { get; protected internal set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();

    public virtual ICollection<Transaction> FromTransactions { get; protected internal set; } = new List<Transaction>();
}
