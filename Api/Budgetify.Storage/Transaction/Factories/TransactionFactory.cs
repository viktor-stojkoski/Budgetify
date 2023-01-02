namespace Budgetify.Storage.Transaction.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.Domain;

internal static class TransactionFactory
{
    /// <summary>
    /// Creates <see cref="Transaction"/> domain entity for a given <see cref="Entities.Transaction"/> storage entity.
    /// </summary>
    internal static Result<Transaction> CreateTransaction(this Entities.Transaction dbTransaction)
    {
        return Transaction.Create(
            id: dbTransaction.Id,
            uid: dbTransaction.Uid,
            createdOn: dbTransaction.CreatedOn,
            deletedOn: dbTransaction.DeletedOn,
            userId: dbTransaction.UserId,
            accountId: dbTransaction.AccountId,
            categoryId: dbTransaction.CategoryId,
            currencyId: dbTransaction.CurrencyId,
            merchantId: dbTransaction.MerchantId,
            type: dbTransaction.Type!,
            amount: dbTransaction.Amount,
            date: dbTransaction.Date,
            description: dbTransaction.Description);
    }

    /// <summary>
    /// Creates <see cref="Entities.Transaction"/> storage entity for a given <see cref="Transaction"/> domain entity.
    /// </summary>
    internal static Entities.Transaction CreateTransaction(this Transaction transaction)
    {
        return new(domainEvents: transaction.DomainEvents)
        {
            Id = transaction.Id,
            Uid = transaction.Uid,
            CreatedOn = transaction.CreatedOn,
            DeletedOn = transaction.DeletedOn,
            UserId = transaction.UserId,
            AccountId = transaction.AccountId,
            CategoryId = transaction.CategoryId,
            CurrencyId = transaction.CurrencyId,
            MerchantId = transaction.MerchantId,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Description = transaction.Description,
        };
    }
}
