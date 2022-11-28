namespace Budgetify.Api.Controllers;

using System.Threading.Tasks;

using Budgetify.Queries.Transaction.Queries.GetTransactions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Queries;

[Route("api/transactions")]
[ApiController]
[Authorize]
public class TransactionsController : ExtendedApiController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public TransactionsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactionsAsync() =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetTransactionsQuery()));
}
