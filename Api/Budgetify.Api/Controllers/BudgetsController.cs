namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Contracts.Budget.Requests;
using Budgetify.Queries.Budget.Queries.GetBudget;
using Budgetify.Queries.Budget.Queries.GetBudgets;
using Budgetify.Services.Budget.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/budgets")]
[ApiController]
[Authorize]
public class BudgetsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public BudgetsController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetBudgetsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetBudgetsQuery()));

    [HttpGet("{budgetUid:Guid}")]
    public async Task<IActionResult> GetBudgetAsync([FromRoute] Guid budgetUid) =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetBudgetQuery(budgetUid)));

    [HttpPost]
    public async Task<IActionResult> CreateBudgetAsync([FromBody] CreateBudgetRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateBudgetCommand(
                Name: request.Name,
                CategoryUid: request.CategoryUid,
                StartDate: request.StartDate,
                EndDate: request.EndDate,
                Amount: request.Amount,
                AmountSpent: request.AmountSpent)));
}
