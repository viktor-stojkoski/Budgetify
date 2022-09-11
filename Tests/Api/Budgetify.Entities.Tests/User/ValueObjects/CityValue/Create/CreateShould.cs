namespace Budgetify.Entities.Tests.User.ValueObjects.CityValue;

using Budgetify.Common.Results;
using Budgetify.Entities.User.ValueObjects;

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
        Result<CityValue> result = CityValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CityInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<CityValue> result = CityValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CityInvalid);
    }

    [Test]
    public void WhenValueHasMoreThan255Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(256);

        // Act
        Result<CityValue> result = CityValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CityInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateCity()
    {
        // Arrange
        string value = "Skopje";

        // Act
        Result<CityValue> result = CityValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
