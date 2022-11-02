namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Contracts.Merchant.Requests;
using Budgetify.Queries.Merchant.Queries.GetMerchants;
using Budgetify.Services.Merchant.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/merchants")]
[ApiController]
[Authorize]
public class MerchantsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public MerchantsController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetMerchantsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetMerchantsQuery()));

    [HttpPost]
    public async Task<IActionResult> CreateMerchantAsync([FromBody] CreateMerchantRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateMerchantCommand(
                Name: request.Name,
                CategoryUid: request.CategoryUid)));

}
