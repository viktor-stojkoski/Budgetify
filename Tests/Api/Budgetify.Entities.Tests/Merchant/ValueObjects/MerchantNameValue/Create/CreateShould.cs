namespace Budgetify.Entities.Tests.Merchant.ValueObjects.MerchantNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Merchant.ValueObjects;

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
        Result<MerchantNameValue> result = MerchantNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.MerchantNameInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<MerchantNameValue> result = MerchantNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.MerchantNameInvalid);
    }

    [Test]
    public void WhenValueHasMoreThan255Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(256);

        // Act
        Result<MerchantNameValue> result = MerchantNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.MerchantNameInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateMerchantName()
    {
        // Arrange
        string value = "Test Merchant Name";

        // Act
        Result<MerchantNameValue> result = MerchantNameValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
