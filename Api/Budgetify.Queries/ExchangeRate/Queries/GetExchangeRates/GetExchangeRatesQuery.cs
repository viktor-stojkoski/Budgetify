namespace Budgetify.Queries.ExchangeRate.Queries.GetExchangeRates;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.ExchangeRate.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetExchangeRatesQuery() : IQuery<IEnumerable<ExchangeRateResponse>>;

public class GetExchangeRatesQueryHandler : IQueryHandler<GetExchangeRatesQuery, IEnumerable<ExchangeRateResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetExchangeRatesQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<ExchangeRateResponse>>> ExecuteAsync(GetExchangeRatesQuery query)
    {
        QueryResultBuilder<IEnumerable<ExchangeRateResponse>> result = new();

        IEnumerable<ExchangeRateResponse> exchangeRates =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<ExchangeRate>()
                .Include(x => x.FromCurrency)
                .Include(x => x.ToCurrency)
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new ExchangeRateResponse(
                    x.Uid,
                    x.FromCurrency.Code,
                    x.ToCurrency.Code,
                    x.FromDate,
                    x.ToDate,
                    x.Rate))
                .ToListAsync();

        result.SetValue(exchangeRates);

        return result.Build();
    }
}
