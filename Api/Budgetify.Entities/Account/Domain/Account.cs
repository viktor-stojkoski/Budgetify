namespace Budgetify.Entities.Account.Domain;

using Budgetify.Entities.Account.Enumerations;
using Budgetify.Entities.Account.ValueObjects;
using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;

public sealed partial class Account : AggregateRoot
{
    public Account(
        int userId,
        AccountNameValue name,
        AccountType type,
        decimal balance,
        int currencyId,
        string? description)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        Name = name;
        Type = type;
        Balance = balance;
        CurrencyId = currencyId;
        Description = description;
    }

    /// <summary>
    /// User that owns this account.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Account's name.
    /// </summary>
    public AccountNameValue Name { get; private set; }

    /// <summary>
    /// Account's type.
    /// </summary>
    public AccountType Type { get; private set; }

    /// <summary>
    /// Account's balance.
    /// </summary>
    public decimal Balance { get; private set; }

    /// <summary>
    /// Account's currency.
    /// </summary>
    public int CurrencyId { get; private set; }

    /// <summary>
    /// Account's description.
    /// </summary>
    public string? Description { get; private set; }
}
