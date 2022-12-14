namespace Budgetify.Storage.ExchangeRate.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.Domain;

internal static class ExchangeRateFactory
{
    /// <summary>
    /// Creates <see cref="ExchangeRate"/> domain entity for a given <see cref="Entities.ExchangeRate"/> storage entity.
    /// </summary>
    internal static Result<ExchangeRate> CreateExchangeRate(this Entities.ExchangeRate dbExchangeRate)
    {
        return ExchangeRate.Create(
            id: dbExchangeRate.Id,
            uid: dbExchangeRate.Uid,
            createdOn: dbExchangeRate.CreatedOn,
            deletedOn: dbExchangeRate.DeletedOn,
            userId: dbExchangeRate.UserId,
            fromCurrencyId: dbExchangeRate.FromCurrencyId,
            toCurrencyId: dbExchangeRate.ToCurrencyId,
            fromDate: dbExchangeRate.FromDate,
            toDate: dbExchangeRate.ToDate,
            rate: dbExchangeRate.Rate);
    }

    /// <summary>
    /// Creates <see cref="Entities.ExchangeRate"/> storage entity for a given <see cref="ExchangeRate"/> domain entity.
    /// </summary>
    internal static Entities.ExchangeRate CreateExchangeRate(this ExchangeRate exchangeRate)
    {
        return new(
            id: exchangeRate.Id,
            uid: exchangeRate.Uid,
            createdOn: exchangeRate.CreatedOn,
            deletedOn: exchangeRate.DeletedOn,
            userId: exchangeRate.UserId,
            fromCurrencyId: exchangeRate.FromCurrencyId,
            toCurrencyId: exchangeRate.ToCurrencyId,
            fromDate: exchangeRate.DateRange.FromDate,
            toDate: exchangeRate.DateRange.ToDate,
            rate: exchangeRate.Rate);
    }
}
