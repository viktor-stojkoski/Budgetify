namespace Budgetify.Storage.Currency.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.Domain;

internal static class CurrencyFactory
{
    /// <summary>
    /// Creates <see cref="Currency"/> domain entity for a given <see cref="Entities.Currency"/> storage entity.
    /// </summary>
    internal static Result<Currency> CreateCurrency(this Entities.Currency dbCurrency)
    {
        return Currency.Create(
            id: dbCurrency.Id,
            uid: dbCurrency.Uid,
            createdOn: dbCurrency.CreatedOn,
            deletedOn: dbCurrency.DeletedOn,
            name: dbCurrency.Name,
            code: dbCurrency.Code,
            symbol: dbCurrency.Symbol);
    }
}
