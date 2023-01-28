namespace Budgetify.Storage.Transaction.Factories;

using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.Domain;

internal static class TransactionAttachmentFactory
{
    /// <summary>
    /// Creates <see cref="TransactionAttachment"/> domain entity for a given <see cref="Entities.TransactionAttachment"/> storage entity.
    /// </summary>
    internal static Result<TransactionAttachment> CreateTransactionAttachment(this Entities.TransactionAttachment dbTransactionAttachment)
    {
        return TransactionAttachment.Create(
            id: dbTransactionAttachment.Id,
            uid: dbTransactionAttachment.Uid,
            createdOn: dbTransactionAttachment.CreatedOn,
            deletedOn: dbTransactionAttachment.DeletedOn,
            transactionId: dbTransactionAttachment.TransactionId,
            filePath: dbTransactionAttachment.FilePath,
            name: dbTransactionAttachment.Name);
    }

    /// <summary>
    /// Creates <see cref="Entities.TransactionAttachment"/> storage entity for a given <see cref="TransactionAttachment"/> domain entity.
    /// </summary>
    internal static Entities.TransactionAttachment CreateTransactionAttachment(this TransactionAttachment transactionAttachment)
    {
        return new(
            id: transactionAttachment.Id,
            uid: transactionAttachment.Uid,
            createdOn: transactionAttachment.CreatedOn,
            deletedOn: transactionAttachment.DeletedOn,
            transactionId: transactionAttachment.TransactionId,
            filePath: transactionAttachment.FilePath,
            name: transactionAttachment.Name);
    }

    /// <summary>
    /// Creates list of <see cref="TransactionAttachment"/> domain entities 
    /// for a given <see cref="Entities.TransactionAttachment"/> storage entity list.
    /// </summary>
    internal static IEnumerable<Result<TransactionAttachment>> CreateTransactionAttachments(
        this IEnumerable<Entities.TransactionAttachment> dbTransactionAttachments)
    {
        return dbTransactionAttachments?.Select(CreateTransactionAttachment)
            ?? Enumerable.Empty<Result<TransactionAttachment>>();
    }
}
