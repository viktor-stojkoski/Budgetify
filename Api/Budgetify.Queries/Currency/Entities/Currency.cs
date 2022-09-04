namespace Budgetify.Queries.Currency.Entities;

using Budgetify.Queries.Common.Entities;

public class Currency : Entity
{
    public Currency(
        string name,
        string code,
        string symbol)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
    }

    public string Name { get; protected internal set; }

    public string Code { get; protected internal set; }

    public string Symbol { get; protected internal set; }
}
