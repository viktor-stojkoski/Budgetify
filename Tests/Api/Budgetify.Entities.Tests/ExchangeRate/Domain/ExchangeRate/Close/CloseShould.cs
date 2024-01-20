namespace Budgetify.Entities.Tests.ExchangeRate.Domain.ExchangeRate;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.ExchangeRate.Domain;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CloseShould))]
public class CloseShould
{
    [Test]
    public void WhenAlreadyClosed_WontOverrideToDate()
    {
        // Arrange
        DateTime closedOn = new(2024, 1, 10);

        ExchangeRate exchangeRate = new ExchangeRateBuilder()
            .WithToDate(new DateTime(2024, 2, 10))
            .Build();

        // Act
        Result result = exchangeRate.Close(closedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        exchangeRate.State.Should().Be(EntityState.Unchanged);
        exchangeRate.DateRange.ToDate.Should().Be(new DateTime(2024, 2, 10));
    }

    [Test]
    public void WhenDateRangeInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime closedOn = new(2024, 1, 1);

        ExchangeRate exchangeRate = new ExchangeRateBuilder()
            .Build();

        // Act
        Result result = exchangeRate.Close(closedOn);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.ExchangeRateFromDateCannotBeGreaterThanToDate);

        exchangeRate.State.Should().Be(EntityState.Unchanged);
        exchangeRate.DateRange.ToDate.Should().BeNull();
    }

    [Test]
    public void WhenArgumentsCorrect_WillCloseTheExchangeRate()
    {
        // Arrange
        DateTime closedOn = new(2024, 2, 1);

        ExchangeRate exchangeRate = new ExchangeRateBuilder()
            .Build();

        // Act
        Result result = exchangeRate.Close(closedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        exchangeRate.State.Should().Be(EntityState.Modified);
        exchangeRate.DateRange.ToDate.Should().Be(closedOn);
    }
}
