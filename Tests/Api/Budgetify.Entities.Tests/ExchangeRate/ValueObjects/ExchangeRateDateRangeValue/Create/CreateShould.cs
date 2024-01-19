namespace Budgetify.Entities.Tests.ExchangeRate.ValueObjects.ExchangeRateDateRangeValue;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenFromDateGreaterThanToDate_WillReturnErrorResult()
    {
        // Arrange
        DateTime fromDate = new(2024, 2, 1);
        DateTime toDate = new(2024, 1, 1);

        // Act
        Result<ExchangeRateDateRangeValue> result =
            ExchangeRateDateRangeValue.Create(fromDate, toDate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.ExchangeRateFromDateCannotBeGreaterThanToDate);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateExchangeRateDateRange()
    {
        // Arrange
        DateTime fromDate = new(2024, 1, 1);
        DateTime toDate = new(2024, 2, 1);

        // Act
        Result<ExchangeRateDateRangeValue> result =
            ExchangeRateDateRangeValue.Create(fromDate, toDate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.FromDate.Should().Be(fromDate);
        result.Value.ToDate.Should().Be(toDate);
    }
}
