namespace Budgetify.Entities.Tests.Account.ValueObjects.AccountNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<AccountNameValue> value1 = AccountNameValue.Create("Test Account Name 1");
        Result<AccountNameValue> value2 = AccountNameValue.Create("Test Account Name 2");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<AccountNameValue> value1 = AccountNameValue.Create("Test Account Name");
        Result<AccountNameValue> value2 = AccountNameValue.Create("Test Account Name");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
