namespace Budgetify.Storage.User.Factories
{
    using Budgetify.Common.Results;
    using Budgetify.Entities.User.Domain;

    internal static class UserFactory
    {
        /// <summary>
        /// Creates <see cref="User"/> domain entity for a given <see cref="Entities.User"/> storage entity.
        /// </summary>
        internal static Result<User> CreateUser(this Entities.User dbUser)
        {
            return User.Create(
                id: dbUser.Id,
                uid: dbUser.Uid,
                createdOn: dbUser.CreatedOn,
                deletedOn: dbUser.DeletedOn,
                name: dbUser.Name,
                email: dbUser.Email);
        }

        /// <summary>
        /// Creates <see cref="Entities.User"/> storage entity for a given <see cref="User"/> domain entity.
        /// </summary>
        internal static Entities.User CreateUser(this User user)
        {
            return new Entities.User
            {
                Id = user.Id,
                Uid = user.Uid,
                CreatedOn = user.CreatedOn,
                DeletedOn = user.DeletedOn,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
