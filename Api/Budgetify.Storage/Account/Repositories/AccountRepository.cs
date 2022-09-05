namespace Budgetify.Storage.Account.Repositories;

using Budgetify.Contracts.Account.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Storage.Account.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

public class AccountRepository : Repository<Entities.Account>, IAccountRepository
{
    public AccountRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Account account)
    {
        Entities.Account dbAccount = account.CreateAccount();

        Insert(dbAccount);
    }

    public void Update(Account account)
    {
        Entities.Account dbAccount = account.CreateAccount();

        AttachOrUpdate(dbAccount, account.State.GetState());
    }
}
