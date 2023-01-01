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
    /// Returns account by given userId and accountUid.
    /// </summary>
    Task<Result<Account>> GetAccountAsync(int userId, Guid accountUid);

    /// <summary>
    /// Returns boolean indicating whether account with the given userId and name exists.
    /// </summary>
    Task<bool> DoesAccountNameExistAsync(int userId, string? name);

    /// <summary>
    /// Returns boolean indicating whether account with the given userId and accountUid is valid for deletion.
    /// </summary>
    Task<bool> IsAccountValidForDeletionAsync(int userId, Guid accountUid);
}
