namespace Budgetify.Storage.Account.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Storage.Account.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

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

    public async Task<Result<Account>> GetAccountAsync(int userId, Guid accountUid)
    {
        Entities.Account? dbAccount = await AllNoTrackedOf<Entities.Account>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == accountUid);

        if (dbAccount is null)
        {
            return Result.NotFound<Account>(ResultCodes.AccountNotFound);
        }

        return dbAccount.CreateAccount();
    }

    public async Task<Result<Account>> GetAccountByIdAsync(int userId, int accountId)
    {
        Entities.Account? dbAccount = await AllNoTrackedOf<Entities.Account>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == accountId);

        if (dbAccount is null)
        {
            return Result.NotFound<Account>(ResultCodes.AccountNotFound);
        }

        return dbAccount.CreateAccount();
    }

    public async Task<bool> DoesAccountNameExistAsync(int userId, string? name)
    {
        return await AllNoTrackedOf<Entities.Account>()
            .AnyAsync(x => x.UserId == userId && x.Name == name);
    }

    public async Task<bool> IsAccountValidForDeletionAsync(int userId, Guid accountUid)
    {
        return await AllNoTrackedOf<Entities.Account>()
            .AnyAsync(x => x.UserId == userId && x.Uid == accountUid
                && !x.Transactions.Any(x => x.DeletedOn == null));
    }

    public async Task<Result<IEnumerable<Account>>> GetAccountsByIdsAsync(int userId, IEnumerable<int> accountIds)
    {
        IEnumerable<Entities.Account> dbAccounts = await AllNoTrackedOf<Entities.Account>()
            .Where(x => x.UserId == userId && accountIds.Contains(x.Id))
            .ToListAsync();

        if (dbAccounts.Count() != accountIds.Count())
        {
            return Result.NotFound<IEnumerable<Account>>(ResultCodes.AccountNotFound);
        }

        IEnumerable<Result<Account>> dbAccountsResults = dbAccounts.CreateAccounts();

        Result dbAccountsResult = Result.FirstFailureNullOrOk(dbAccountsResults);

        if (dbAccountsResult.IsFailureOrNull)
        {
            return Result.FromError<IEnumerable<Account>>(dbAccountsResult);
        }

        return Result.Ok(dbAccountsResults.Select(x => x.Value));
    }
}
