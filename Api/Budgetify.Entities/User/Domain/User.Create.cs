namespace Budgetify.Entities.User.Domain
{
    using System;

    using Budgetify.Common.Results;
    using Budgetify.Entities.User.ValueObjects;

    public partial class User
    {
        /// <summary>
        /// Create user DB to domain only.
        /// </summary>
        public static Result<User> Create(
            int id,
            Guid uid,
            DateTime createdOn,
            DateTime? deletedOn,
            string? name,
            string? email)
        {
            Result<UserNameValue> nameValue = UserNameValue.Create(name);
            Result<EmailValue> emailValue = EmailValue.Create(email);

            Result okOrError = Result.FirstFailureNullOrOk(nameValue, emailValue);

            if (okOrError.IsFailureOrNull)
            {
                return Result.FromError<User>(okOrError);
            }

            return Result.Ok(
                new User(nameValue.Value, emailValue.Value)
                {
                    Id = id,
                    Uid = uid,
                    CreatedOn = createdOn,
                    DeletedOn = deletedOn
                });
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        public static Result<User> Create(
            DateTime createdOn,
            string name,
            string email)
        {
            Result<UserNameValue> nameValue = UserNameValue.Create(name);
            Result<EmailValue> emailValue = EmailValue.Create(email);

            Result okOrError = Result.FirstFailureOrOk(nameValue, emailValue);

            if (okOrError.IsFailure)
            {
                return Result.FromError<User>(okOrError);
            }

            return Result.Ok(
                new User(nameValue.Value, emailValue.Value)
                {
                    Uid = Guid.NewGuid(),
                    CreatedOn = createdOn
                });
        }
    }
}
