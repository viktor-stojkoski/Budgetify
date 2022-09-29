namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.Currency.Queries.GetCurrencies;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/currencies")]
[Authorize]
[ApiController]
public class CurrenciesController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public CurrenciesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrenciesAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetCurrenciesQuery()));
}
