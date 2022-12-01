namespace Budgetify.Storage.Transaction.Repositories;

using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.Transaction.Factories;

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
}
