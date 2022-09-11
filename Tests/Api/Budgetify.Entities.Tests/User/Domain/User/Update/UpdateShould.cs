namespace Budgetify.Entities.Tests.User.Domain.User;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.User.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(UpdateShould))]
public class UpdateShould
{
    [Test]
    public void WhenEmailInvalid_WillReturnErrorResult()
    {
        // Arrange
        string email = "email@invalid";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = "Skopje";

        User user = new UserBuilder()
            .Build();

        // Act
        Result result = user.Update(
            email: email,
            firstName: firstName,
            lastName: lastName,
            city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.EmailInvalid);

        user.State.Should().Be(EntityState.Unchanged);
        user.Email.Value.Should().Be("email@email.com");
        user.FirstName.Value.Should().Be("FirstName");
        user.LastName.Value.Should().Be("LastName");
        user.City.Value.Should().Be("City");
    }

    [Test]
    public void WhenFirstNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        string email = "viktor@budgetify.tech";
        string firstName = RandomString(256);
        string lastName = "Stojkoski";
        string city = "Skopje";

        User user = new UserBuilder()
            .Build();

        // Act
        Result result = user.Update(
            email: email,
            firstName: firstName,
            lastName: lastName,
            city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalidLength);

        user.State.Should().Be(EntityState.Unchanged);
        user.Email.Value.Should().Be("email@email.com");
        user.FirstName.Value.Should().Be("FirstName");
        user.LastName.Value.Should().Be("LastName");
        user.City.Value.Should().Be("City");
    }

    [Test]
    public void WhenLastNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = RandomString(256);
        string city = "Skopje";

        User user = new UserBuilder()
            .Build();

        // Act
        Result result = user.Update(
            email: email,
            firstName: firstName,
            lastName: lastName,
            city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalidLength);

        user.State.Should().Be(EntityState.Unchanged);
        user.Email.Value.Should().Be("email@email.com");
        user.FirstName.Value.Should().Be("FirstName");
        user.LastName.Value.Should().Be("LastName");
        user.City.Value.Should().Be("City");
    }

    [Test]
    public void WhenCityInvalid_WillReturnErrorResult()
    {
        // Arrange
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = RandomString(256);

        User user = new UserBuilder()
            .Build();

        // Act
        Result result = user.Update(
            email: email,
            firstName: firstName,
            lastName: lastName,
            city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CityInvalidLength);

        user.State.Should().Be(EntityState.Unchanged);
        user.Email.Value.Should().Be("email@email.com");
        user.FirstName.Value.Should().Be("FirstName");
        user.LastName.Value.Should().Be("LastName");
        user.City.Value.Should().Be("City");
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateUser()
    {
        // Arrange
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = "Skopje";

        User user = new UserBuilder()
            .Build();

        // Act
        Result result = user.Update(
            email: email,
            firstName: firstName,
            lastName: lastName,
            city: city);

        // Assert
        result.IsSuccess.Should().BeTrue();

        user.State.Should().Be(EntityState.Modified);
        user.Email.Value.Should().Be(email);
        user.FirstName.Value.Should().Be(firstName);
        user.LastName.Value.Should().Be(lastName);
        user.City.Value.Should().Be(city);
    }
}
