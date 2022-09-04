namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencyCodeValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<CurrencyCodeValue> value1 = CurrencyCodeValue.Create("MKD");
        Result<CurrencyCodeValue> value2 = CurrencyCodeValue.Create("USD");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<CurrencyCodeValue> value1 = CurrencyCodeValue.Create("MKD");
        Result<CurrencyCodeValue> value2 = CurrencyCodeValue.Create("MKD");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
