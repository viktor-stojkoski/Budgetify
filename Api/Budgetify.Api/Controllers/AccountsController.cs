namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Contracts.Account.Requests;
using Budgetify.Queries.Account.Queries.GetAccounts;
using Budgetify.Services.Account.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/accounts")]
[Authorize]
[ApiController]
public class AccountsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public AccountsController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetAccountsQuery()));

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateAccountCommand(
                Name: request.Name,
                Type: request.Type,
                Balance: request.Balance,
                CurrencyCode: request.CurrencyCode,
                Description: request.Description)));
}
