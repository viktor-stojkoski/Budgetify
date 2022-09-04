namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencyCodeValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenValueNotThreeCharacters_WillReturnErrorResult()
    {
        // Arrange
        string value = "MK";

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenValueHasNumbers_WillReturnErrorResult()
    {
        // Arrange
        string value = "MK1";

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenValueHasLowerCharacters_WillReturnErrorResult()
    {
        // Arrange
        string value = "Mkd";

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenValueCorrect_WillCreateCurrencyCode()
    {
        // Arrange
        string value = "MKD";

        // Act
        Result<CurrencyCodeValue> result = CurrencyCodeValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Value.Should().Be(value);
    }
}
