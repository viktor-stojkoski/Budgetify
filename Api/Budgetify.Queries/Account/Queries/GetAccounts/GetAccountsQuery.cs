namespace Budgetify.Queries.Account.Queries.GetAccounts;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetAccountsQuery() : IQuery<IEnumerable<AccountResponse>>;

public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, IEnumerable<AccountResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetAccountsQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<AccountResponse>>> ExecuteAsync(GetAccountsQuery query)
    {
        QueryResultBuilder<IEnumerable<AccountResponse>> result = new();

        IEnumerable<AccountResponse> accounts =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Account>()
                .Include(x => x.Currency)
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new AccountResponse(
                    x.Uid,
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
