namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencyNameValue;

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
        Result<CurrencyNameValue> value1 = CurrencyNameValue.Create("Macedonian Denar");
        Result<CurrencyNameValue> value2 = CurrencyNameValue.Create("Euro");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<CurrencyNameValue> value1 = CurrencyNameValue.Create("Macedonian Denar");
        Result<CurrencyNameValue> value2 = CurrencyNameValue.Create("Macedonian Denar");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
