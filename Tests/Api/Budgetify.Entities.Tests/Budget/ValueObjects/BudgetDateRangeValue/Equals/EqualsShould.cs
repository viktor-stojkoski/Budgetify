namespace Budgetify.Entities.Tests.Budget.ValueObjects.BudgetDateRangeValue;

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
        Result<BudgetDateRangeValue> value1 =
            BudgetDateRangeValue.Create(
                startDate: new(2024, 1, 1),
                endDate: new(2024, 2, 1));

        Result<BudgetDateRangeValue> value2 =
            BudgetDateRangeValue.Create(
                startDate: new(2024, 1, 1),
                endDate: new(2024, 3, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<BudgetDateRangeValue> value1 =
            BudgetDateRangeValue.Create(
                startDate: new(2024, 1, 1),
                endDate: new(2024, 2, 1));

        Result<BudgetDateRangeValue> value2 =
            BudgetDateRangeValue.Create(
                startDate: new(2024, 1, 1),
                endDate: new(2024, 2, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
