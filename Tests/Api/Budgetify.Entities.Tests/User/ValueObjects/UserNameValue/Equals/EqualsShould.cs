namespace Budgetify.Entities.Tests.User.ValueObjects.UserNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.User.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<UserNameValue> value1 = UserNameValue.Create("Viktor");
        Result<UserNameValue> value2 = UserNameValue.Create("Zoran");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<UserNameValue> value1 = UserNameValue.Create("Viktor");
        Result<UserNameValue> value2 = UserNameValue.Create("Viktor");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
