namespace Budgetify.Entities.Tests.Budget.ValueObjects.BudgetNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<BudgetNameValue> value1 = BudgetNameValue.Create("Test Budget Name 1");
        Result<BudgetNameValue> value2 = BudgetNameValue.Create("Test Budget Name 2");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<BudgetNameValue> value1 = BudgetNameValue.Create("Test Budget Name");
        Result<BudgetNameValue> value2 = BudgetNameValue.Create("Test Budget Name");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
