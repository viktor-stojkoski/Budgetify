namespace Budgetify.Entities.ExchangeRate.Domain;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.ExchangeRate.ValueObjects;

public sealed partial class ExchangeRate : AggregateRoot
{
    public ExchangeRate(
        int userId,
        int fromCurrencyId,
        int toCurrencyId,
        ExchangeRateDateRangeValue dateRange,
        decimal rate)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        FromCurrencyId = fromCurrencyId;
        ToCurrencyId = toCurrencyId;
        DateRange = dateRange;
        Rate = rate;
    }

    /// <summary>
    /// User that owns this exchange rate.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Exchange rate's from currency.
    /// </summary>
    public int FromCurrencyId { get; private set; }

    /// <summary>
    /// Exchange rate's to currency.
    /// </summary>
    public int ToCurrencyId { get; private set; }

    /// <summary>
    /// Exchange rate's date range.
    /// </summary>
    public ExchangeRateDateRangeValue DateRange { get; private set; }

    /// <summary>
    /// Exchange rate's rate.
    /// </summary>
    public decimal Rate { get; private set; }
}
