namespace Budgetify.Queries.Budget.Queries.GetBudgets;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.Budget.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetBudgetsQuery : IQuery<IEnumerable<BudgetResponse>>;

public class GetBudgetsQueryHandler : IQueryHandler<GetBudgetsQuery, IEnumerable<BudgetResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetBudgetsQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<BudgetResponse>>> ExecuteAsync(GetBudgetsQuery query)
    {
        QueryResultBuilder<IEnumerable<BudgetResponse>> result = new();

        IEnumerable<BudgetResponse> budgets =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Budget>()
                .Include(x => x.Category)
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new BudgetResponse(
                    x.Uid,
                    x.Name,
                    x.Category.Name,
                    x.StartDate,
                    x.EndDate,
                    x.Amount,
                    x.AmountSpent))
                .ToListAsync();

        result.SetValue(budgets);

        return result.Build();
    }
}
