namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Storage;
using Budgetify.Contracts.Transaction.Requests;
using Budgetify.Queries.Transaction.Queries.GetTransaction;
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

    [HttpGet("{transactionUid:Guid}")]
    public async Task<IActionResult> GetTransactionAsync([FromRoute] Guid transactionUid) =>
        OkOrError(await _queryDispatcher.ExecuteAsync(new GetTransactionQuery(transactionUid)));

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new CreateTransactionCommand(
                AccountUid: request.AccountUid,
                FromAccountUid: request.FromAccountUid,
                CategoryUid: request.CategoryUid,
                CurrencyCode: request.CurrencyCode,
                MerchantUid: request.MerchantUid,
                Type: request.Type,
                Amount: request.Amount,
                Date: request.Date,
                Description: request.Description,
                Attachments: request.Attachments)));

    [HttpPost("create-by-scan")]
    public async Task<IActionResult> CreateTransactionByScanningAsync([FromBody] FileForUploadRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(new CreateTransactionByScanCommand(request)));

    [HttpPut("{transactionUid:Guid}")]
    public async Task<IActionResult> UpdateTransactionAsync(
        [FromRoute] Guid transactionUid,
        [FromBody] UpdateTransactionRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new UpdateTransactionCommand(
                TransactionUid: transactionUid,
                AccountUid: request.AccountUid,
                CategoryUid: request.CategoryUid,
                CurrencyCode: request.CurrencyCode,
                MerchantUid: request.MerchantUid,
                Type: request.Type,
                Amount: request.Amount,
                Date: request.Date,
                Description: request.Description)));

    [HttpPatch("{transactionUid:Guid}/verify")]
    public async Task<IActionResult> VerifyTransactionAsync(Guid transactionUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(new VerifyTransactionCommand(transactionUid)));

    [HttpDelete("{transactionUid:Guid}")]
    public async Task<IActionResult> DeleteTransactionAsync([FromRoute] Guid transactionUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(new DeleteTransactionCommand(transactionUid)));
}
