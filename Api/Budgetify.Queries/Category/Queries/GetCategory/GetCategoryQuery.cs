namespace Budgetify.Queries.Category.Queries.GetCategory;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Category.Entities;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetCategoryQuery(Guid CategoryUid) : IQuery<CategoryResponse>;

public class GetCategoryCommandHandler : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetCategoryCommandHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<CategoryResponse>> ExecuteAsync(GetCategoryQuery query)
    {
        QueryResultBuilder<CategoryResponse> result = new();

        CategoryResponse? category =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Category>()
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.CategoryUid)
                .Select(x => new CategoryResponse(
                    x.Name,
                    x.Type))
                .SingleOrDefaultAsync();

        if (category is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.CategoryNotFound));
        }

        result.SetValue(category);

        return result.Build();
    }
}
