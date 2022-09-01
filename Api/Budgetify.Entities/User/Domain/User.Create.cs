namespace Budgetify.Entities.User.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
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
        string email,
        string firstName,
        string lastName,
        string city)
    {
        Result<EmailValue> emailValue = EmailValue.Create(email);
        Result<UserNameValue> firstNameValue = UserNameValue.Create(firstName);
        Result<UserNameValue> lastNameValue = UserNameValue.Create(lastName);
        Result<CityValue> cityValue = CityValue.Create(city);

        Result okOrError =
            Result.FirstFailureNullOrOk(
                emailValue,
                firstNameValue,
                lastNameValue,
                cityValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<User>(okOrError);
        }

        return Result.Ok(
            new User(
                email: emailValue.Value,
                firstName: firstNameValue.Value,
                lastName: lastNameValue.Value,
                city: cityValue.Value)
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
        string? email,
        string? firstName,
        string? lastName,
        string? city)
    {
        Result<EmailValue> emailValue = EmailValue.Create(email);
        Result<UserNameValue> firstNameValue = UserNameValue.Create(firstName);
        Result<UserNameValue> lastNameValue = UserNameValue.Create(lastName);
        Result<CityValue> cityValue = CityValue.Create(city);

        Result okOrError =
            Result.FirstFailureNullOrOk(
                emailValue,
                firstNameValue,
                lastNameValue,
                cityValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<User>(okOrError);
        }

        return Result.Ok(
            new User(
                email: emailValue.Value,
                firstName: firstNameValue.Value,
                lastName: lastNameValue.Value,
                city: cityValue.Value)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
