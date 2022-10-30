namespace Budgetify.Queries.Account.Queries.GetAccount;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetAccountQuery(Guid Uid) : IQuery<AccountResponse>;

public class GetAccountQueryHandler : IQueryHandler<GetAccountQuery, AccountResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetAccountQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<AccountResponse>> ExecuteAsync(GetAccountQuery query)
    {
        QueryResultBuilder<AccountResponse> result = new();

        AccountResponse? account =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Account>()
                .Include(x => x.Currency)
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.Uid)
                .Select(x => new AccountResponse(
                    x.Name,
                    x.Type,
                    x.Balance,
                    x.Currency.Code,
                    x.Description))
                .SingleOrDefaultAsync();

        if (account is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.AccountNotFound));
        }

        result.SetValue(account);

        return result.Build();
    }
}
