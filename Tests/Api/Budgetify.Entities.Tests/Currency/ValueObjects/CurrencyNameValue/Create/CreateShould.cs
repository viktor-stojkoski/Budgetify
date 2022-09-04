namespace Budgetify.Entities.Tests.Currency.ValueObjects.CurrencyNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<CurrencyNameValue> result = CurrencyNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyNameInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<CurrencyNameValue> result = CurrencyNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyNameInvalid);
    }

    [Test]
    public void WhenValueHasMoreThanFiftyCharacters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(51);

        // Act
        Result<CurrencyNameValue> result = CurrencyNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyNameInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateCurrencyName()
    {
        // Arrange
        string value = "Macedonian Denar";

        // Act
        Result<CurrencyNameValue> result = CurrencyNameValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Value.Should().Be(value);
    }
}
