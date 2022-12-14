namespace Budgetify.Contracts.ExchangeRate.Repositories;

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
    /// Returns exchange rate from the given currencies id.
    /// </summary>
    Task<Result<ExchangeRate>> GetExchangeRateByCurrenciesWithoutToDate(int fromCurrencyId, int toCurrencyId);
}
