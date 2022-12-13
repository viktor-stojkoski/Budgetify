namespace Budgetify.Storage.ExchangeRate.Repositories;

using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.ExchangeRate.Factories;
using Budgetify.Storage.Infrastructure.Context;

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
}
