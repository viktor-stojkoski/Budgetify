namespace Budgetify.Entities.Tests.ExchangeRate.ValueObjects.ExchangeRateDateRangeValue;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenBothDatesNullAndNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(null, null);

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: new(2024, 2, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenBothDatesNullAndEqual_WillReturnTrue()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(null, null);

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(null, null);

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void WhenFromDateNullAndNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: null,
                toDate: new(2024, 1, 1));

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: null,
                toDate: new(2024, 2, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenFromDateNullAndEqual_WillReturnTrue()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: null,
                toDate: new(2024, 1, 1));

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: null,
                toDate: new(2024, 1, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void WhenToDateNullAndNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: null);

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 2, 1),
                toDate: null);

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenToDateNullAndEqual_WillReturnTrue()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: null);

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: null);

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void WhenBothNotNullAndNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: new(2024, 2, 1));

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: new(2024, 3, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenBothNotNullAndEqual_WillReturnTrue()
    {
        // Arrange
        Result<ExchangeRateDateRangeValue> value1 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: new(2024, 2, 1));

        Result<ExchangeRateDateRangeValue> value2 =
            ExchangeRateDateRangeValue.Create(
                fromDate: new(2024, 1, 1),
                toDate: new(2024, 2, 1));

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
