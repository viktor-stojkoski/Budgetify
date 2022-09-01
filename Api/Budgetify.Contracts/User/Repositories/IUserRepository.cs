namespace Budgetify.Contracts.User.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Entities.User.Domain;

public interface IUserRepository
{
    /// <summary>
    /// Returns user by given userUid.
    /// </summary>
    Task<Result<User>> GetUserAsync(Guid userUid);

    /// <summary>
    /// Inserts new user.
    /// </summary>
    void Insert(User user);

    /// <summary>
    /// Updates user.
    /// </summary>
    void Update(User user);
}
