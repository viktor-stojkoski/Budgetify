namespace Budgetify.Entities.Tests.Category.Domain.Category;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 17);
        int userId = RandomId();
        string name = RandomString(256);
        string type = "EXPENSE";

        // Act
        Result<Category> result =
            Category.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryNameInvalidLength);
    }

    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 17);
        int userId = RandomId();
        string name = "Name";
        string type = "INVALID";

        // Act
        Result<Category> result =
            Category.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryTypeInvalid);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateCategory()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 17);
        int userId = RandomId();
        string name = "Name";
        string type = "EXPENSE";

        // Act
        Result<Category> result =
            Category.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Added);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.Name.Value.Should().Be(name);
        result.Value.Type.Name.Should().Be(type);
    }
}
