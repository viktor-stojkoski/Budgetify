namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

using Budgetify.Contracts.Transaction.Requests;
using Budgetify.Services.Transaction.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VS.Commands;

[Route("api/transactions/{transactionUid:Guid}/attachments")]
[ApiController]
[Authorize]
public class TransactionAttachmentsController : ExtendedApiController
{
    private readonly ICommandDispatcher _commandDispatcher;

    public TransactionAttachmentsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> AddTransactionAttachmentsAsync(
        [FromRoute] Guid transactionUid,
        [FromBody] AddTransactionAttachmentsRequest request) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new AddTransactionAttachmentsCommand(
                TransactionUid: transactionUid,
                Attachments: request.Attachments)));

    [HttpDelete("{attachmentUid:Guid}")]
    public async Task<IActionResult> DeleteTransactionAttachmentAsync(
        [FromRoute] Guid transactionUid,
        [FromRoute] Guid attachmentUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new DeleteTransactionAttachmentCommand(
                TransactionUid: transactionUid,
                AttachmentUid: attachmentUid)));
}
