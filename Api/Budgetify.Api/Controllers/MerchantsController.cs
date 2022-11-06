namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Contracts.Merchant.Requests;
using Budgetify.Queries.Merchant.Queries.GetMerchant;
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

    [HttpGet("{merchantUid:Guid}")]
    public async Task<IActionResult> GetMerchantAsync(Guid merchantUid) =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetMerchantQuery(merchantUid)));

    [HttpPost]
    public async Task<IActionResult> CreateMerchantAsync([FromBody] CreateMerchantRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateMerchantCommand(
                Name: request.Name,
                CategoryUid: request.CategoryUid)));

    [HttpPut("{merchantUid:Guid}")]
    public async Task<IActionResult> UpdateMerchantAsync(
        [FromRoute] Guid merchantUid,
        [FromBody] UpdateMerchantRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new UpdateMerchantCommand(
                MerchantUid: merchantUid,
                Name: request.Name,
                CategoryUid: request.CategoryUid)));

    [HttpDelete("{merchantUid:Guid}")]
    public async Task<IActionResult> DeleteMerchantAsync([FromRoute] Guid merchantUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(new DeleteMerchantCommand(merchantUid)));
}
