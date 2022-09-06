namespace Budgetify.Storage.Currency.Repositories;

using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Currency.Factories;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

public class CurrencyRepository : Repository<Entities.Currency>, ICurrencyRepository
{
    public CurrencyRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public async Task<Result<Currency>> GetCurrencyByCodeAsync(string? code)
    {
        Entities.Currency? dbCurrency = await AllNoTrackedOf<Entities.Currency>()
            .SingleOrDefaultAsync(x => x.Code == code);

        if (dbCurrency is null)
        {
            return Result.NotFound<Currency>(ResultCodes.CurrencyNotFound);
        }

        return dbCurrency.CreateCurrency();
    }
}
