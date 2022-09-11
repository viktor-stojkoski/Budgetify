namespace Budgetify.Entities.Tests.User.ValueObjects.CityValue;

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
        Result<CityValue> value1 = CityValue.Create("Skopje");
        Result<CityValue> value2 = CityValue.Create("Tetovo");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenEqual_WillReturnTrue()
    {
        // Arrange
        Result<CityValue> value1 = CityValue.Create("Skopje");
        Result<CityValue> value2 = CityValue.Create("Skopje");

        // Act
        bool result = value1.Value == value2.Value;

        // Assert
        result.Should().BeTrue();
    }
}
