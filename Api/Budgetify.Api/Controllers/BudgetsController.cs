namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.Budget.Queries.GetBudgets;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/budgets")]
[ApiController]
[Authorize]
public class BudgetsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public BudgetsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetButgetsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetBudgetsQuery()));
}
