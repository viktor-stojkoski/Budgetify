namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.ExchangeRate.Queries.GetExchangeRates;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/exchange-rates")]
[ApiController]
[Authorize]
public class ExchangeRatesController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public ExchangeRatesController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetExchangeRatesAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetExchangeRatesQuery()));
}
