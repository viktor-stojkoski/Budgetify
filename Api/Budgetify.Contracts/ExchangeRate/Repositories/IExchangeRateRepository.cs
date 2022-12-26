namespace Budgetify.Contracts.ExchangeRate.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.Domain;

public interface IExchangeRateRepository
{
    /// <summary>
    /// Inserts new exchange rate.
    /// </summary>
    void Insert(ExchangeRate exchangeRate);

    /// <summary>
    /// Updates exchange rate.
    /// </summary>
    void Update(ExchangeRate exchangeRate);

    /// <summary>
    /// Returns exchange rate by given userId and exchangeRateUid.
    /// </summary>
    Task<Result<ExchangeRate>> GetExchangeRateAsync(int userId, Guid exchangeRateUid);

    /// <summary>
    /// Returns exchange rate from the given currencies id without to date.
    /// </summary>
    Task<Result<ExchangeRate>> GetExchangeRateByCurrenciesWithoutToDate(int userId, int fromCurrencyId, int toCurrencyId);

    /// <summary>
    /// Returns exchange rate from the given currencies id.
    /// </summary>
    Task<Result<ExchangeRate>> GetLastClosedExchangeRateByCurrencies(int userId, int fromCurrencyId, int toCurrencyId);
}
