namespace Budgetify.Storage.Transaction.Entities;

using System;

using Budgetify.Storage.Common.Entities;

public class TransactionAttachment : Entity
{
    protected internal TransactionAttachment(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int transactionId,
        string filePath,
        string name) : base(id, uid, createdOn, deletedOn)
    {
        TransactionId = transactionId;
        FilePath = filePath;
        Name = name;
    }

    public int TransactionId { get; protected internal set; }

    public string FilePath { get; protected internal set; }

    public string Name { get; protected internal set; }

    public Transaction Transaction { get; protected internal set; } = null!;
}
