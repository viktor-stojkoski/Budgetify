namespace Budgetify.Storage.Account.Factories;

using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;

internal static class AccountFactory
{
    /// <summary>
    /// Creates <see cref="Account"/> domain entity for a given <see cref="Entities.Account"/> storage entity.
    /// </summary>
    internal static Result<Account> CreateAccount(this Entities.Account dbAccount)
    {
        return Account.Create(
            id: dbAccount.Id,
            uid: dbAccount.Uid,
            createdOn: dbAccount.CreatedOn,
            deletedOn: dbAccount.DeletedOn,
            userId: dbAccount.UserId,
            name: dbAccount.Name,
            type: dbAccount.Type,
            balance: dbAccount.Balance,
            currencyId: dbAccount.CurrencyId,
            description: dbAccount.Description);
    }

    /// <summary>
    /// Creates <see cref="Entities.Account"/> storage entity for a given <see cref="Account"/> domain entity.
    /// </summary>
    internal static Entities.Account CreateAccount(this Account account)
    {
        return new(
            id: account.Id,
            uid: account.Uid,
            createdOn: account.CreatedOn,
            deletedOn: account.DeletedOn,
            userId: account.UserId,
            name: account.Name,
            type: account.Type,
            balance: account.Balance,
            currencyId: account.CurrencyId,
            description: account.Description);
    }

    /// <summary>
    /// Creates list of <see cref="Account"/> domain entities for a given <see cref="Entities.Account"/> storage entity list.
    /// </summary>
    internal static IEnumerable<Result<Account>> CreateAccounts(this IEnumerable<Entities.Account> dbAccounts)
    {
        return dbAccounts?.Select(CreateAccount) ?? Enumerable.Empty<Result<Account>>();
    }
}
