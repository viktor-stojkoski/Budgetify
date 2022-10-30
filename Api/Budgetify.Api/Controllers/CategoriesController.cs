namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.Category.Queries.GetCategories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/categories")]
[Authorize]
[ApiController]
public class CategoriesController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public CategoriesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetCategoriesQuery()));
}
