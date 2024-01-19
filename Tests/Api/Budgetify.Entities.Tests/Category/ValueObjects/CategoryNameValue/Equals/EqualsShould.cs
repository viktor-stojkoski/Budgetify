namespace Budgetify.Entities.Tests.Category.ValueObjects.CategoryNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<CategoryNameValue> value1 = CategoryNameValue.Create("Test Category Name 1");
        Result<CategoryNameValue> value2 = CategoryNameValue.Create("Test Category Name 2");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<CategoryNameValue> value1 = CategoryNameValue.Create("Test Category Name");
        Result<CategoryNameValue> value2 = CategoryNameValue.Create("Test Category Name");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
