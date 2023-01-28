namespace Budgetify.Storage.Transaction.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.Transaction.Factories;

using Microsoft.EntityFrameworkCore;

public class TransactionRepository : Repository<Entities.Transaction>, ITransactionRepository
{
    public TransactionRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Transaction transaction)
    {
        Entities.Transaction dbTransaction = transaction.CreateTransaction();

        foreach (TransactionAttachment transactionAttachment in transaction.Attachments)
        {
            Entities.TransactionAttachment dbTransactionAttachment =
                transactionAttachment.CreateTransactionAttachment();

            dbTransactionAttachment.Transaction = dbTransaction;
            dbTransaction.TransactionAttachments.Add(dbTransactionAttachment);

            Insert(dbTransactionAttachment);
        }

        Insert(dbTransaction);
    }

    public void Update(Transaction transaction)
    {
        Entities.Transaction dbTransaction = transaction.CreateTransaction();

        foreach (TransactionAttachment transactionAttachment in transaction.Attachments)
        {
            AttachOrUpdate(
                transactionAttachment.CreateTransactionAttachment(),
                transactionAttachment.State.GetState());
        }

        AttachOrUpdate(dbTransaction, transaction.State.GetState());
    }

    public async Task<Result<Transaction>> GetTransactionAsync(int userId, Guid transactionUid)
    {
        Entities.Transaction? dbTransaction = await AllNoTrackedOf<Entities.Transaction>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == transactionUid);

        if (dbTransaction is null)
        {
            return Result.NotFound<Transaction>(ResultCodes.TransactionNotFound);
        }

        return dbTransaction.CreateTransaction();
    }

    public async Task<Result<Transaction>> GetTransactionWithAttachmentsAsync(int userId, Guid transactionUid)
    {
        Entities.Transaction? dbTransaction = await AllNoTrackedOf<Entities.Transaction>()
            .Include(x => x.TransactionAttachments)
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == transactionUid);

        if (dbTransaction is null)
        {
            return Result.NotFound<Transaction>(ResultCodes.TransactionNotFound);
        }

        return dbTransaction.CreateTransaction();
    }

    public async Task<Result<IEnumerable<Transaction>>> GetTransactionsWithConversionsInDateRangeAsync(int userId, DateTime? fromDate, DateTime? toDate)
    {
        IEnumerable<Entities.Transaction> dbTransactions =
            await AllNoTrackedOf<Entities.Transaction>()
                .Include(x => x.Account)
                .Where(x => x.UserId == userId
                    && (!fromDate.HasValue || fromDate.Value.Date <= x.Date)
                        && (!toDate.HasValue || toDate.Value.Date >= x.Date)
                            && x.Account.CurrencyId != x.CurrencyId)
                .ToListAsync();

        IEnumerable<Result<Transaction>> dbTransactionsResults = dbTransactions.CreateTransactions();

        Result dbTransactionsResult = Result.FirstFailureNullOrOk(dbTransactionsResults);

        if (dbTransactionsResult.IsFailureOrNull)
        {
            return Result.FromError<IEnumerable<Transaction>>(dbTransactionsResult);
        }

        return Result.Ok(dbTransactionsResults.Select(x => x.Value));
    }
}
