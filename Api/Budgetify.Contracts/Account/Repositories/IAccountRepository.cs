namespace Budgetify.Contracts.Account.Repositories;

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
}
