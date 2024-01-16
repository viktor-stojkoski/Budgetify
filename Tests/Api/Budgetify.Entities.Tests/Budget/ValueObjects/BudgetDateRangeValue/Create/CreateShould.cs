namespace Budgetify.Entities.Tests.Budget.ValueObjects.BudgetDateRangeValue;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenStartDateGreaterThanEndDate_WillReturnErrorResult()
    {
        // Arrange
        DateTime startDate = new(2024, 2, 1);
        DateTime endDate = new(2024, 1, 1);

        // Act
        Result<BudgetDateRangeValue> result =
            BudgetDateRangeValue.Create(startDate, endDate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetStartDateCannotBeGreaterThanEndDate);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateBudgetDateRange()
    {
        // Arrange
        DateTime startDate = new(2024, 1, 1);
        DateTime endDate = new(2024, 2, 1);

        // Act
        Result<BudgetDateRangeValue> result =
            BudgetDateRangeValue.Create(startDate, endDate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.StartDate.Should().Be(startDate);
        result.Value.EndDate.Should().Be(endDate);
    }
}
