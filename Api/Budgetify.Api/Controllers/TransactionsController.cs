namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Contracts.Transaction.Requests;
using Budgetify.Queries.Transaction.Queries.GetTransactions;
using Budgetify.Services.Transaction.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;
using VS.Queries;

[Route("api/transactions")]
[ApiController]
[Authorize]
public class TransactionsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public TransactionsController(
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetTransactionsQuery()));

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateTransactionCommand(
                AccountUid: request.AccountUid,
                CategoryUid: request.CategoryUid,
                CurrencyCode: request.CurrencyCode,
                MerchantUid: request.MerchantUid,
                Type: request.Type,
                Amount: request.Amount,
                Date: request.Date,
                Description: request.Description)));
}
