namespace Budgetify.Contracts.Transaction.Requests;

using System;
using System.Collections.Generic;

using Budgetify.Common.Storage;

public class CreateTransactionRequest
{
    public Guid AccountUid { get; set; }

    public Guid CategoryUid { get; set; }

    public string? CurrencyCode { get; set; }

    public Guid? MerchantUid { get; set; }

    public string? Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public IEnumerable<FileForUploadRequest> Attachments { get; set; } = new List<FileForUploadRequest>();
}
