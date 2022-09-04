namespace Budgetify.Storage.Account.Entities;

using System;

using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Currency.Entities;
using Budgetify.Storage.User.Entities;

public class Account : AggregateRoot
{
    public Account(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userFk,
        string name,
        string type,
        decimal balance,
        int currencyFk,
        string? description) : base(id, uid, createdOn, deletedOn)
    {
        UserFk = userFk;
        Name = name;
        Type = type;
        Balance = balance;
        CurrencyFk = currencyFk;
        Description = description;
    }

    public int UserFk { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Type { get; protected internal set; }

    public decimal Balance { get; protected internal set; }

    public int CurrencyFk { get; protected internal set; }

    public string? Description { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Currency Currency { get; protected internal set; } = null!;
}
