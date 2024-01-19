namespace Budgetify.Entities.Tests.Category.ValueObjects.CategoryNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<CategoryNameValue> result = CategoryNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryNameInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<CategoryNameValue> result = CategoryNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryNameInvalid);
    }

    [Test]
    public void WhenValueHasMoreThan255Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(256);

        // Act
        Result<CategoryNameValue> result = CategoryNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryNameInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateCategoryName()
    {
        // Arrange
        string value = "Test Category Name";

        // Act
        Result<CategoryNameValue> result = CategoryNameValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
