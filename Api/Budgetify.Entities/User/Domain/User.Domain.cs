namespace Budgetify.Entities.User.Domain;

using Budgetify.Common.Results;
using Budgetify.Entities.User.ValueObjects;

public partial class User
{
    /// <summary>
    /// Updates user.
    /// </summary>
    public Result Update(
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
            return okOrError;
        }

        Email = emailValue.Value;
        FirstName = firstNameValue.Value;
        LastName = lastNameValue.Value;
        City = cityValue.Value;

        MarkModify();

        return Result.Ok();
    }
}
