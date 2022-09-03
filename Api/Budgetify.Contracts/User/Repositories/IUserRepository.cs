namespace Budgetify.Contracts.User.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Entities.User.Domain;

public interface IUserRepository
{
    /// <summary>
    /// Inserts new user.
    /// </summary>
    void Insert(User user);

    /// <summary>
    /// Updates user.
    /// </summary>
    void Update(User user);

    /// <summary>
    /// Returns user by given userUid.
    /// </summary>
    Task<Result<User>> GetUserAsync(Guid userUid);

    /// <summary>
    /// Returns boolean indicating whether user with the given email exists.
    /// </summary>
    Task<bool> DoesUserWithEmailExists(string? email);
}
