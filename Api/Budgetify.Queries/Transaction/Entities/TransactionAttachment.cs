namespace Budgetify.Queries.Transaction.Entities;

using Budgetify.Queries.Common.Entities;

public class TransactionAttachment : Entity
{
    protected internal TransactionAttachment(int transactionId, string filePath, string name)
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
