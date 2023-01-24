namespace Budgetify.Queries.Transaction.Queries.GetTransaction;

using System;

public class TransactionAttachmentResponse
{
    public TransactionAttachmentResponse(Guid uid, DateTime createdOn, string name, Uri url)
    {
        Uid = uid;
        Name = name;
        Url = url;
        CreatedOn = createdOn;
    }

    public Guid Uid { get; set; }

    public DateTime CreatedOn { get; set; }

    public string Name { get; set; }

    public Uri Url { get; set; }
}
