namespace Budgetify.Queries.Currency.Entities;

using System.Collections.Generic;

using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.ExchangeRate.Entities;
using Budgetify.Queries.Transaction.Entities;

public class Currency : Entity
{
    public Currency(
        string name,
        string code,
        string? symbol)
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

    public virtual ICollection<ExchangeRate> ExchangeRateFromCurrencies { get; protected internal set; } = new List<ExchangeRate>();

    public virtual ICollection<ExchangeRate> ExchangeRateToCurrencies { get; protected internal set; } = new List<ExchangeRate>();
}
