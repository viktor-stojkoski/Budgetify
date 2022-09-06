namespace Budgetify.Queries.Account.Entities;

using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Currency.Entities;
using Budgetify.Queries.User.Entities;

public class Account : Entity
{
    public Account(
        int userId,
        string name,
        string type,
        decimal balance,
        int currencyId,
        string? description)
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
}
