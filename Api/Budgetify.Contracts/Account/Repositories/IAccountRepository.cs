namespace Budgetify.Contracts.Account.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;

public interface IAccountRepository
{
    /// <summary>
    /// Inserts new account.
    /// </summary>
    void Insert(Account account);

    /// <summary>
    /// Updates account.
    /// </summary>
    void Update(Account account);

    /// <summary>
    /// Returns account by given accountUid.
    /// </summary>
    Task<Result<Account>> GetAccountAsync(Guid accountUid);
}
