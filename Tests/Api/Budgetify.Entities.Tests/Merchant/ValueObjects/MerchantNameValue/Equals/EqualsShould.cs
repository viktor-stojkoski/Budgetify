namespace Budgetify.Entities.Tests.Merchant.ValueObjects.MerchantNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Merchant.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(EqualsShould))]
public class EqualsShould
{
    [Test]
    public void WhenNotEqual_WillReturnFalse()
    {
        // Arrange
        Result<MerchantNameValue> value1 = MerchantNameValue.Create("Test Merchant Name 1");
        Result<MerchantNameValue> value2 = MerchantNameValue.Create("Test Merchant Name 2");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<MerchantNameValue> value1 = MerchantNameValue.Create("Test Merchant Name");
        Result<MerchantNameValue> value2 = MerchantNameValue.Create("Test Merchant Name");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
