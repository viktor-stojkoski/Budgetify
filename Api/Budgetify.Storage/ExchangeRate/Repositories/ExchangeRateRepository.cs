namespace Budgetify.Storage.ExchangeRate.Repositories;

using System;
using System.Linq;
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

    public void Insert(ExchangeRate exchangeRate)
    {
        Entities.ExchangeRate dbExchangeRate = exchangeRate.CreateExchangeRate();

        Insert(dbExchangeRate);
    }

    public void Update(ExchangeRate exchangeRate)
    {
        Entities.ExchangeRate dbExchangeRate = exchangeRate.CreateExchangeRate();

        AttachOrUpdate(dbExchangeRate, exchangeRate.State.GetState());
    }

    public async Task<Result<ExchangeRate>> GetExchangeRateAsync(int userId, Guid exchangeRateUid)
    {
        Entities.ExchangeRate? dbExchangeRate = await AllNoTrackedOf<Entities.ExchangeRate>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == exchangeRateUid);

        if (dbExchangeRate is null)
        {
            return Result.NotFound<ExchangeRate>(ResultCodes.ExchangeRateNotFound);
        }

        return dbExchangeRate.CreateExchangeRate();
    }

    public async Task<Result<ExchangeRate>> GetExchangeRateByCurrenciesWithoutToDate(int userId, int fromCurrencyId, int toCurrencyId)
    {
        Entities.ExchangeRate? dbExchangeRate = await AllNoTrackedOf<Entities.ExchangeRate>()
            .SingleOrDefaultAsync(x => x.UserId == userId
                && x.FromCurrencyId == fromCurrencyId
                    && x.ToCurrencyId == toCurrencyId
                        && !x.ToDate.HasValue);

        if (dbExchangeRate is null)
        {
            return Result.NotFound<ExchangeRate>(ResultCodes.ExchangeRateNotFound);
        }

        return dbExchangeRate.CreateExchangeRate();
    }

    public async Task<Result<ExchangeRate>> GetLastClosedExchangeRateByCurrencies(int userId, int fromCurrencyId, int toCurrencyId)
    {
        Entities.ExchangeRate? dbExchangeRate = await AllNoTrackedOf<Entities.ExchangeRate>()
            .Where(x => x.UserId == userId
                && x.FromCurrencyId == fromCurrencyId
                    && x.ToCurrencyId == toCurrencyId
                        && x.ToDate.HasValue)
            .OrderByDescending(x => x.ToDate)
            .FirstOrDefaultAsync();

        if (dbExchangeRate is null)
        {
            return Result.NotFound<ExchangeRate>(ResultCodes.ExchangeRateNotFound);
        }

        return dbExchangeRate.CreateExchangeRate();
    }
}
