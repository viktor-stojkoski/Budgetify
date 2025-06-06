﻿namespace Budgetify.Contracts.Account.Repositories;

using System;
using System.Collections.Generic;
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
    /// Returns account by given userId and accountId.
    /// </summary>
    Task<Result<Account>> GetAccountByIdAsync(int userId, int accountId);

    /// <summary>
    /// Returns boolean indicating whether account with the given userId and name exists.
    /// </summary>
    Task<bool> DoesAccountNameExistAsync(int userId, Guid accountUid, string? name);

    /// <summary>
    /// Returns boolean indicating whether account with the given userId and accountUid is valid for deletion.
    /// </summary>
    Task<bool> IsAccountValidForDeletionAsync(int userId, Guid accountUid);

    /// <summary>
    /// Returns accounts with the given userId and accountIds.
    /// </summary>
    Task<Result<IEnumerable<Account>>> GetAccountsByIdsAsync(int userId, IEnumerable<int> accountIds);
}
