namespace Budgetify.Queries.ExchangeRate.Queries.GetExchangeRate;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.ExchangeRate.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetExchangeRateQuery(Guid ExchangeRateUid) : IQuery<ExchangeRateResponse>;

public class GetExchangeRateQueryHandler : IQueryHandler<GetExchangeRateQuery, ExchangeRateResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetExchangeRateQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<ExchangeRateResponse>> ExecuteAsync(GetExchangeRateQuery query)
    {
        QueryResultBuilder<ExchangeRateResponse> result = new();

        ExchangeRateResponse? exchangeRate =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<ExchangeRate>()
                .Include(x => x.FromCurrency)
                .Include(x => x.ToCurrency)
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.ExchangeRateUid)
                .Select(x => new ExchangeRateResponse(
                    x.FromCurrency.Code,
                    x.ToCurrency.Code,
                    x.FromDate,
                    x.ToDate,
                    x.Rate))
                .SingleOrDefaultAsync();

        if (exchangeRate is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.ExchangeRateNotFound));
        }

        result.SetValue(exchangeRate);

        return result.Build();
    }
}
