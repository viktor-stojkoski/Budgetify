namespace Budgetify.Entities.Currency.Domain;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Currency.ValueObjects;

public sealed partial class Currency : AggregateRoot
{
    private Currency(
        CurrencyNameValue name,
        CurrencyCodeValue code,
        CurrencySymbolValue symbol)
    {
        State = EntityState.Unchanged;

        Name = name;
        Code = code;
        Symbol = symbol;
    }

    /// <summary>
    /// Currency's name.
    /// </summary>
    public CurrencyNameValue Name { get; private set; }

    /// <summary>
    /// Currency's code.
    /// </summary>
    public CurrencyCodeValue Code { get; private set; }

    /// <summary>
    /// Currency's symbol.
    /// </summary>
    public CurrencySymbolValue Symbol { get; private set; }
}
