namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Contracts.ExchangeRate.Requests;
using Budgetify.Queries.ExchangeRate.Queries.GetExchangeRate;
using Budgetify.Queries.ExchangeRate.Queries.GetExchangeRates;
using Budgetify.Services.ExchangeRate.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/exchange-rates")]
[ApiController]
[Authorize]
public class ExchangeRatesController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public ExchangeRatesController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetExchangeRatesAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetExchangeRatesQuery()));

    [HttpGet("{exchangeRateUid:Guid}")]
    public async Task<IActionResult> GetExchangeRateAsync([FromRoute] Guid exchangeRateUid) =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetExchangeRateQuery(exchangeRateUid)));

    [HttpPost]
    public async Task<IActionResult> CreateExchangeRateAsync([FromBody] CreateExchangeRateRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateExchangeRateCommand(
                FromCurrencyCode: request.FromCurrencyCode,
                ToCurrencyCode: request.ToCurrencyCode,
                FromDate: request.FromDate,
                Rate: request.Rate)));

    [HttpPut]
    public async Task<IActionResult> UpdateExchangeRateAsync([FromBody] UpdateExchangeRateRequest request) =>
    OkOrError(await _commandDispatcher.ExecuteAsync(
        new UpdateExchangeRateCommand(request.FromDate, request.Rate)));
}
