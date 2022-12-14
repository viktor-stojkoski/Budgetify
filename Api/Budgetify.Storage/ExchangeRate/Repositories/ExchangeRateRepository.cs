namespace Budgetify.Storage.ExchangeRate.Repositories;

using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.ExchangeRate.Factories;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

public class ExchangeRateRepository : Repository<Entities.ExchangeRate>, IExchangeRateRepository
{
    public ExchangeRateRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Budgetify.Entities.ExchangeRate.Domain.ExchangeRate exchangeRate)
    {
        Entities.ExchangeRate dbExchangeRate = exchangeRate.CreateExchangeRate();

        Insert(dbExchangeRate);
    }

    public void Update(Budgetify.Entities.ExchangeRate.Domain.ExchangeRate exchangeRate)
    {
        Entities.ExchangeRate dbExchangeRate = exchangeRate.CreateExchangeRate();

        AttachOrUpdate(dbExchangeRate, exchangeRate.State.GetState());
    }

    public async Task<Result<ExchangeRate>> GetExchangeRateByCurrencies(int fromCurrencyId, int toCurrencyId)
    {
        Entities.ExchangeRate? dbExchangeRate = await AllNoTrackedOf<Entities.ExchangeRate>()
            .SingleOrDefaultAsync(x => x.FromCurrencyId == fromCurrencyId && x.ToCurrencyId == toCurrencyId);

        if (dbExchangeRate is null)
        {
            return Result.NotFound<ExchangeRate>(ResultCodes.ExchangeRateNotFound);
        }

        return dbExchangeRate.CreateExchangeRate();
    }
}
