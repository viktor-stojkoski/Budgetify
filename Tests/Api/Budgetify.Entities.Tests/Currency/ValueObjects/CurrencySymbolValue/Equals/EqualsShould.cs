namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencySymbolValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenOneValueNull_WillReturnFalse()
    {
        // Arrange
        Result<CurrencySymbolValue> value1 = CurrencySymbolValue.Create(null);
        Result<CurrencySymbolValue> value2 = CurrencySymbolValue.Create("$");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<CurrencySymbolValue> value1 = CurrencySymbolValue.Create("ден");
        Result<CurrencySymbolValue> value2 = CurrencySymbolValue.Create("$");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<CurrencySymbolValue> currencyCodeValue1 = CurrencySymbolValue.Create("ден");
        Result<CurrencySymbolValue> currencyCodeValue2 = CurrencySymbolValue.Create("ден");

        // Act
        bool result = currencyCodeValue1.Value == currencyCodeValue2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
