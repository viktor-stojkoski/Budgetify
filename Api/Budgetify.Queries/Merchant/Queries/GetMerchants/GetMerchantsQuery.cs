namespace Budgetify.Queries.Merchant.Queries.GetMerchants;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.Infrastructure.Context;
using Budgetify.Queries.Merchant.Entities;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetMerchantsQuery() : IQuery<IEnumerable<MerchantResponse>>;

public class GetMerchantsQueryHandler : IQueryHandler<GetMerchantsQuery, IEnumerable<MerchantResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetMerchantsQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<MerchantResponse>>> ExecuteAsync(GetMerchantsQuery query)
    {
        QueryResultBuilder<IEnumerable<MerchantResponse>> result = new();

        IEnumerable<MerchantResponse> merchants =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Merchant>()
                .Include(x => x.Category)
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new MerchantResponse(
                    x.Uid,
                    x.Name,
                    x.Category.Name))
                .ToListAsync();

        result.SetValue(merchants);

        return result.Build();
    }
}
