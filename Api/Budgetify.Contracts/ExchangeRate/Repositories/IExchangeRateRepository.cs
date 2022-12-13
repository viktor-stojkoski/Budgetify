namespace Budgetify.Contracts.ExchangeRate.Repositories;

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
}
