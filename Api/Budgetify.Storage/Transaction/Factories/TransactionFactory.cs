namespace Budgetify.Storage.Transaction.Factories;

using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.Domain;

internal static class TransactionFactory
{
    /// <summary>
    /// Creates <see cref="Transaction"/> domain entity for a given <see cref="Entities.Transaction"/> storage entity.
    /// </summary>
    internal static Result<Transaction> CreateTransaction(this Entities.Transaction dbTransaction)
    {
        IEnumerable<Result<TransactionAttachment>> attachmentsResults =
            dbTransaction.TransactionAttachments.CreateTransactionAttachments();

        Result attachmentsResult = Result.FirstFailureNullOrOk(attachmentsResults);

        if (attachmentsResult.IsFailureOrNull)
        {
            return Result.FromError<Transaction>(attachmentsResult);
        }

        return Transaction.Create(
            id: dbTransaction.Id,
            uid: dbTransaction.Uid,
            createdOn: dbTransaction.CreatedOn,
            deletedOn: dbTransaction.DeletedOn,
            userId: dbTransaction.UserId,
            accountId: dbTransaction.AccountId,
            fromAccountId: dbTransaction.FromAccountId,
            categoryId: dbTransaction.CategoryId,
            currencyId: dbTransaction.CurrencyId,
            merchantId: dbTransaction.MerchantId,
            type: dbTransaction.Type,
            amount: dbTransaction.Amount,
            date: dbTransaction.Date,
            description: dbTransaction.Description,
            isVerified: dbTransaction.IsVerified,
            attachments: attachmentsResults.Select(x => x.Value));
    }

    /// <summary>
    /// Creates <see cref="Entities.Transaction"/> storage entity for a given <see cref="Transaction"/> domain entity.
    /// </summary>
    internal static Entities.Transaction CreateTransaction(this Transaction transaction)
    {
        return new(
            id: transaction.Id,
            uid: transaction.Uid,
            createdOn: transaction.CreatedOn,
            deletedOn: transaction.DeletedOn,
            domainEvents: transaction.DomainEvents,
            userId: transaction.UserId,
            accountId: transaction.AccountId,
            fromAccountId: transaction.FromAccountId,
            categoryId: transaction.CategoryId,
            currencyId: transaction.CurrencyId,
            merchantId: transaction.MerchantId,
            type: transaction.Type,
            amount: transaction.Amount,
            date: transaction.Date,
            description: transaction.Description,
            isVerified: transaction.IsVerified);
    }

    /// <summary>
    /// Creates list of <see cref="Transaction"/> domain entities for a given <see cref="Entities.Transaction"/> storage entity list.
    /// </summary>
    internal static IEnumerable<Result<Transaction>> CreateTransactions(this IEnumerable<Entities.Transaction> dbTransactions)
    {
        return dbTransactions?.Select(CreateTransaction) ?? Enumerable.Empty<Result<Transaction>>();
    }
}
