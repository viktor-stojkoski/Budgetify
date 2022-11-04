namespace Budgetify.Queries.Merchant.Queries.GetMerchant;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.Infrastructure.Context;
using Budgetify.Queries.Merchant.Entities;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetMerchantQuery(Guid MerchantUid) : IQuery<MerchantResponse>;

public class GetMerchantQueryHandler : IQueryHandler<GetMerchantQuery, MerchantResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetMerchantQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<MerchantResponse>> ExecuteAsync(GetMerchantQuery query)
    {
        QueryResultBuilder<MerchantResponse> result = new();

        MerchantResponse? merchant =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Merchant>()
                .Include(x => x.Category)
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.MerchantUid)
                .Select(x => new MerchantResponse(
                    x.Name,
                    x.Category.Uid,
                    x.Category.Name))
                .SingleOrDefaultAsync();

        if (merchant is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.MerchantNotFound));
        }

        result.SetValue(merchant);

        return result.Build();
    }
}
