namespace Budgetify.Queries.Budget.Queries.GetBudget;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Budget.Entities;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetBudgetQuery(Guid BudgetUid) : IQuery<BudgetResponse>;

public class GetBudgetQueryHandler : IQueryHandler<GetBudgetQuery, BudgetResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetBudgetQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<BudgetResponse>> ExecuteAsync(GetBudgetQuery query)
    {
        QueryResultBuilder<BudgetResponse> result = new();

        BudgetResponse? budget =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Budget>()
                .Include(x => x.Category)
                .Include(x => x.Currency)
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.BudgetUid)
                .Select(x => new BudgetResponse(
                    x.Name,
                    x.Category.Name,
                    x.Currency.Code,
                    x.StartDate,
                    x.EndDate,
                    x.Amount,
                    x.AmountSpent))
                .SingleOrDefaultAsync();

        if (budget is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.BudgetNotFound));
        }

        result.SetValue(budget);

        return result.Build();
    }
}
