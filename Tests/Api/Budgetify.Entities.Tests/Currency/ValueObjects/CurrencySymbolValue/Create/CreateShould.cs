namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencySymbolValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueHasMoreThan10Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(11);

        // Act
        Result<CurrencySymbolValue> result = CurrencySymbolValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencySymbolInvalidLength);
    }

    [Test]
    public void WhenValueNull_WillCreateEmptyCurrencySymbol()
    {
        // Arrange
        string? value = null;

        // Act
        Result<CurrencySymbolValue> result = CurrencySymbolValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Value.Should().Be(value);
    }

    [Test]
    public void WhenValueEmptyString_WillCreateEmptyCurrencySymbol()
    {
        // Arrange
        string value = "";

        // Act
        Result<CurrencySymbolValue> result = CurrencySymbolValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Value.Should().Be(value);
    }

    [Test]
    public void WhenValueCorrect_WillCreateCurrencySymbol()
    {
        // Arrange
        string value = "ден";

        // Act
        Result<CurrencySymbolValue> result = CurrencySymbolValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string? implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
