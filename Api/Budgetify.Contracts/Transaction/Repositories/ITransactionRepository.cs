namespace Budgetify.Contracts.Transaction.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
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

    /// <summary>
    /// Returns transaction by given userId and transactionUid.
    /// </summary>
    Task<Result<Transaction>> GetTransactionAsync(int userId, Guid transactionUid);

    /// <summary>
    /// Returns transactions in the given date range.
    /// </summary>
    Task<Result<IEnumerable<Transaction>>> GetTransactionsInDateRangeAsync(int userId, DateTime? fromDate, DateTime? toDate);
}
