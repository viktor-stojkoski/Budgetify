namespace Budgetify.Queries.Category.Queries.GetCategories;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.Category.Entities;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetCategoriesQuery() : IQuery<IEnumerable<CategoryResponse>>;

public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetCategoriesQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<CategoryResponse>>> ExecuteAsync(GetCategoriesQuery query)
    {
        QueryResultBuilder<IEnumerable<CategoryResponse>> result = new();

        IEnumerable<CategoryResponse> categories =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Category>()
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new CategoryResponse(
                    x.Uid,
                    x.Name,
                    x.Type))
                .ToListAsync();

        result.SetValue(categories);

        return result.Build();
    }
}
