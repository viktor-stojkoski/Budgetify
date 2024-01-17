namespace Budgetify.Entities.Tests.Category.Enumerations;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<CategoryType> result = CategoryType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryTypeInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<CategoryType> result = CategoryType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryTypeInvalid);
    }

    [Test]
    public void WhenValueDoesNotExist_WillReturnErrorResult()
    {
        // Arrange
        string value = "RANDOM_VALUE";

        // Act
        Result<CategoryType> result = CategoryType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryTypeInvalid);
    }

    [Test]
    [TestCase(1, "EXPENSE")]
    [TestCase(2, "INCOME")]
    public void WhenValueCorrect_WillCreateCategoryType(int id, string name)
    {
        // Arrange

        // Act
        Result<CategoryType> result = CategoryType.Create(name);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(name);
        result.Value.Id.Should().Be(id);
    }
}
