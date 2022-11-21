namespace Budgetify.Storage.Currency.Entities;

using System;
using System.Collections.Generic;

using Budgetify.Storage.Account.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Transaction.Entities;

public class Currency : AggregateRoot
{
    public Currency(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        string name,
        string code,
        string? symbol) : base(id, uid, createdOn, deletedOn)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
    }

    public string Name { get; protected internal set; }

    public string Code { get; protected internal set; }

    public string? Symbol { get; protected internal set; }

    public virtual ICollection<Account> Accounts { get; protected internal set; } = new List<Account>();

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();
}
