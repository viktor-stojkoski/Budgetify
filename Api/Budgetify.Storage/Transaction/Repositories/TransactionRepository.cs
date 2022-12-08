namespace Budgetify.Storage.Transaction.Repositories;

using System;
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

        Insert(dbTransaction);
    }

    public void Update(Transaction transaction)
    {
        Entities.Transaction dbTransaction = transaction.CreateTransaction();

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
}
