namespace Budgetify.Queries.Account.Queries.GetAccount;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetAccountQuery(Guid Uid) : IQuery<AccountResponse>;

public class GetAccountQueryHandler : IQueryHandler<GetAccountQuery, AccountResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;

    public GetAccountQueryHandler(IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
    }

    public async Task<QueryResult<AccountResponse>> ExecuteAsync(GetAccountQuery query)
    {
        QueryResultBuilder<AccountResponse> result = new();

        AccountResponse? account =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Account>()
                .Include(x => x.Currency)
                .Where(x => x.UserId == 1 && x.Uid == query.Uid) // TODO: Fix the hardcoded 1 by implementing Current User logic
                .Select(x => new AccountResponse(
                    x.Name,
                    x.Type,
                    x.Balance,
                    x.Currency.Code,
                    x.Description))
                .SingleOrDefaultAsync();

        if (account is null)
        {
            return result.FailWith(ResultCodes.AccountNotFound);
        }

        result.SetValue(account);

        return result.Build();
    }
}
