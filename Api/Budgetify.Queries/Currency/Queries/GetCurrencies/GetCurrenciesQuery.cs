namespace Budgetify.Queries.Currency.Queries.GetCurrencies;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Queries.Currency.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetCurrenciesQuery() : IQuery<IEnumerable<CurrencyResponse>>;

public class GetCurrenciesQueryHandler : IQueryHandler<GetCurrenciesQuery, IEnumerable<CurrencyResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;

    public GetCurrenciesQueryHandler(IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
    }

    public async Task<QueryResult<IEnumerable<CurrencyResponse>>> ExecuteAsync(GetCurrenciesQuery query)
    {
        QueryResultBuilder<IEnumerable<CurrencyResponse>> result = new();

        IEnumerable<CurrencyResponse> currencies =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Currency>()
                .Select(x => new CurrencyResponse(
                    x.Name,
                    x.Code,
                    x.Symbol))
                .ToListAsync();

        result.SetValue(currencies);

        return result.Build();
    }
}
