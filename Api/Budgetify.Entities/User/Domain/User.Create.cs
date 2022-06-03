namespace Budgetify.Entities.User.Domain
{
    using System;
    using System.Collections.Generic;

    using Budgetify.Common.Results;
    using Budgetify.Entities.User.ValueObjects;

    public partial class User
    {
        /// <summary>
        /// Create User DB to domain only.
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

            //nameValue.FailureOrOk(emailValue);

            //test.AnyFailure();
            //Result okOrError = Result.FirstFailureOrOk(nameValue, emailValue);
            Result okOrError = Result.FirstFailureNullOrOk(nameValue, emailValue);

            //Result ok = ResultExtensions.IsNull(nameValue, emailValue, test2);

            //if (nameValue.IsFailureOrNull || emailValue.IsFailureOrNull)
            //{
            //    return Result.FromError<User>(okOrError);
            //}

            //if (nameValue.AreFailureOrNull(emailValue))
            //{

            //}

            if (okOrError.IsFailure)
            {
                return Result.FromError<User>(okOrError);
            }

            return Result.Ok(
                new User(nameValue.Value, emailValue.Value)
                {
                    Id = id,
                    Uid = uid,
                    CreatedOn = createdOn,
                    DeletedOn = deletedOn,
                    Name = nameValue.Value,
                    Email = emailValue.Value
                });
        }

        /// <summary>
        /// Creates User.
        /// </summary>
        public static Result<User> Create(
            Guid uid,
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
                    Uid = uid,
                    CreatedOn = createdOn,
                    Name = nameValue.Value,
                    Email = emailValue.Value
                });
        }
    }
}
