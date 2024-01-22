namespace Budgetify.Entities.Tests.Transaction.Domain;

using System.Collections.Generic;

using TransactionAttachmentEntity = Entities.Transaction.Domain.TransactionAttachment;

internal class DataHelper
{
    internal static IEnumerable<TransactionAttachmentEntity> CreateTransactionAttachments(
        int transactionId = 1)
    {
        return new List<TransactionAttachmentEntity>()
        {
            TransactionAttachmentEntity.Create(
                createdOn: new(2024, 1, 20),
                transactionId: transactionId,
                filePath: "transactions/path-1",
                name: "Test name 1").Value,

            TransactionAttachmentEntity.Create(
                createdOn: new(2024, 1, 21),
                transactionId: transactionId,
                filePath: "transactions/path-2",
                name: "Test name 2").Value,

            TransactionAttachmentEntity.Create(
                createdOn: new(2024, 1, 22),
                transactionId: transactionId,
                filePath: "transactions/path-3",
                name: "Test name 3").Value
        };
    }
}
