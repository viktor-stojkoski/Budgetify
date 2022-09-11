namespace Budgetify.Entities.Tests.User.Domain.User;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.User.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateFromStorageShould))]
public class CreateFromStorageShould
{
    [Test]
    public void WhenEmailInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 11);
        string email = "wrong_email";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = "Skopje";

        // Act
        Result<User> result =
            User.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                email: email,
                firstName: firstName,
                lastName: lastName,
                city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.EmailInvalid);
    }

    [Test]
    public void WhenFirstNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 11);
        string email = "viktor@budgetify.tech";
        string firstName = RandomString(256);
        string lastName = "Stojkoski";
        string city = "Skopje";

        // Act
        Result<User> result =
            User.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                email: email,
                firstName: firstName,
                lastName: lastName,
                city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalidLength);
    }

    [Test]
    public void WhenLastNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 11);
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = RandomString(256);
        string city = "Skopje";

        // Act
        Result<User> result =
            User.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                email: email,
                firstName: firstName,
                lastName: lastName,
                city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalidLength);
    }

    [Test]
    public void WhenCityInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 11);
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = RandomString(256);

        // Act
        Result<User> result =
            User.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                email: email,
                firstName: firstName,
                lastName: lastName,
                city: city);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CityInvalidLength);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateUserFromStorage()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 11);
        string email = "viktor@budgetify.tech";
        string firstName = "Viktor";
        string lastName = "Stojkoski";
        string city = "Skopje";

        // Act
        Result<User> result =
            User.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                email: email,
                firstName: firstName,
                lastName: lastName,
                city: city);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Unchanged);
        result.Value.Id.Should().Be(id);
        result.Value.Uid.Should().Be(uid);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.Email.Value.Should().Be(email);
        result.Value.FirstName.Value.Should().Be(firstName);
        result.Value.LastName.Value.Should().Be(lastName);
        result.Value.City.Value.Should().Be(city);
    }
}
