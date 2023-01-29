namespace Budgetify.Contracts.Transaction.Requests;

using System.Collections.Generic;

using Budgetify.Common.Storage;

public class AddTransactionAttachmentsRequest
{
    public IEnumerable<FileForUploadRequest> Attachments { get; set; } = new List<FileForUploadRequest>();
}
