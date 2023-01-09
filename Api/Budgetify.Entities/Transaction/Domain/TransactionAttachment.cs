namespace Budgetify.Entities.Transaction.Domain;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.ValueObjects;

public sealed partial class TransactionAttachment : Entity
{
    public TransactionAttachment(
        int transactionId,
        FilePathValue filePath,
        TransactionAttachmentNameValue name)
    {
        State = EntityState.Unchanged;

        TransactionId = transactionId;
        FilePath = filePath;
        Name = name;
    }

    /// <summary>
    /// Transaction that has this attachment.
    /// </summary>
    public int TransactionId { get; private set; }

    /// <summary>
    /// Transaction attachment's file path.
    /// </summary>
    public FilePathValue FilePath { get; private set; }

    /// <summary>
    /// Transaction attachment's name.
    /// </summary>
    public TransactionAttachmentNameValue Name { get; private set; }
}
