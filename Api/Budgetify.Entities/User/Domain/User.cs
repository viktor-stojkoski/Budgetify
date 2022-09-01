namespace Budgetify.Entities.User.Domain;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.User.ValueObjects;

public sealed partial class User : AggregateRoot
{
    private User(
        EmailValue email,
        UserNameValue firstName,
        UserNameValue lastName,
        CityValue city)
    {
        State = EntityState.Unchanged;

        Email = email;
        FirstName = firstName;
        LastName = lastName;
        City = city;
    }

    /// <summary>
    /// User's email address.
    /// </summary>
    public EmailValue Email { get; private set; }

    /// <summary>
    /// User's first name.
    /// </summary>
    public UserNameValue FirstName { get; private set; }

    /// <summary>
    /// User's last name.
    /// </summary>
    public UserNameValue LastName { get; private set; }

    /// <summary>
    /// User's city.
    /// </summary>
    public CityValue City { get; private set; }
}
