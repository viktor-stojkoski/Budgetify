namespace Budgetify.Entities.Tests.User.ValueObjects.UserNameValue;

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
        Result<UserNameValue> result = UserNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<UserNameValue> result = UserNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalid);
    }

    [Test]
    public void WhenValueHasMoreThan255Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(256);

        // Act
        Result<UserNameValue> result = UserNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.UserNameInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateUserName()
    {
        // Arrange
        string value = "Viktor";

        // Act
        Result<UserNameValue> result = UserNameValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
