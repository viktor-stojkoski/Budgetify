namespace Budgetify.Contracts.Currency.Repositories;

using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.Domain;

public interface ICurrencyRepository
{
    /// <summary>
    /// Returns currency by the given currency code.
    /// </summary>
    Task<Result<Currency>> GetCurrencyByCodeAsync(string? code);
}
