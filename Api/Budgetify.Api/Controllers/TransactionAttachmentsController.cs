namespace Budgetify.Api.Controllers;

using System;
using System.Threading.Tasks;

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

    [HttpDelete("{attachmentUid:Guid}")]
    public async Task<IActionResult> DeleteTransactionAttachmentAsync(
        [FromRoute] Guid transactionUid,
        [FromRoute] Guid attachmentUid) =>
        OkOrError(await _commandDispatcher.ExecuteAsync(
            new DeleteTransactionAttachmentCommand(
                TransactionUid: transactionUid,
                AttachmentUid: attachmentUid)));
}
