namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.Merchant.Queries.GetMerchants;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/merchants")]
[ApiController]
[Authorize]
public class MerchantsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public MerchantsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetMerchantsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetMerchantsQuery()));
}
