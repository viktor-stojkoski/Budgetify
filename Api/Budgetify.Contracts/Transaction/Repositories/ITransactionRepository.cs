namespace Budgetify.Contracts.Transaction.Repositories;

using Budgetify.Entities.Transaction.Domain;

public interface ITransactionRepository
{
    /// <summary>
    /// Inserts new transaction.
    /// </summary>
    void Insert(Transaction transaction);

    /// <summary>
    /// Updates transaction.
    /// </summary>
    void Update(Transaction transaction);
}
