namespace Budgetify.Queries.Account.Queries.GetAccounts;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetAccountsQuery() : IQuery<IEnumerable<AccountResponse>>;

public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, IEnumerable<AccountResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;

    public GetAccountsQueryHandler(IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
    }

    public async Task<QueryResult<IEnumerable<AccountResponse>>> ExecuteAsync(GetAccountsQuery query)
    {
        QueryResultBuilder<IEnumerable<AccountResponse>> result = new();

        IEnumerable<AccountResponse> accounts =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Account>()
                .Include(x => x.Currency)
                .Where(x => x.UserId == 1) // TODO: Fix the hardcoded 1 by implementing Current User logic
                .Select(x => new AccountResponse(
                    x.Name,
                    x.Type,
                    x.Balance,
                    x.Currency.Code,
                    x.Description))
                .ToListAsync();

        result.SetValue(accounts);

        return result.Build();
    }
}
