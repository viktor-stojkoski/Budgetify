namespace Budgetify.Entities.Tests.User.ValueObjects.EmailValue;

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
        Result<EmailValue> value1 = EmailValue.Create("viktor@budgetify.tech");
        Result<EmailValue> value2 = EmailValue.Create("zoran@budgetify.tech");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<EmailValue> value1 = EmailValue.Create("viktor@budgetify.tech");
        Result<EmailValue> value2 = EmailValue.Create("viktor@budgetify.tech");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
