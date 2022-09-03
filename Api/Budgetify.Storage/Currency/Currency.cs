namespace Budgetify.Storage.Currency;

using System;

using Budgetify.Storage.Common.Entities;

public class Currency : AggregateRoot
{
    public Currency(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        string name,
        string code,
        string symbol) : base(id, uid, createdOn, deletedOn)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
    }

    public string Name { get; protected internal set; }

    public string Code { get; protected internal set; }

    public string Symbol { get; protected internal set; }
}
