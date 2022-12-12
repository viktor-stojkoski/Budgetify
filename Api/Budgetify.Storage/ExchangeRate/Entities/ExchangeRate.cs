namespace Budgetify.Storage.ExchangeRate.Entities;

using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Currency.Entities;
using Budgetify.Storage.User.Entities;

public class ExchangeRate : AggregateRoot
{
    public ExchangeRate(
        int userId,
        int fromCurrencyId,
        int toCurrencyId,
        decimal rate)
    {
        UserId = userId;
        FromCurrencyId = fromCurrencyId;
        ToCurrencyId = toCurrencyId;
        Rate = rate;
    }

    public int UserId { get; protected internal set; }

    public int FromCurrencyId { get; protected internal set; }

    public int ToCurrencyId { get; protected internal set; }

    public decimal Rate { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Currency FromCurrency { get; protected internal set; } = null!;

    public virtual Currency ToCurrency { get; protected internal set; } = null!;
}
