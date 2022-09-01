namespace Budgetify.Common.Tests.Extensions.StringExtensions;

using Budgetify.Common.Extensions;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(HasValueShould))]
public class HasValueShould
{
    [Test]
    public void WhenNonEmptyString_WillReturnTrue()
    {
        // Arrange
        string str = "Test string";

        // Act
        bool result = str.HasValue();

        // Assert
        result.Should().BeTrue();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("    ")]
    public void WhenWhitespaceAndEmptyString_WillReturnFalse(string str)
    {
        // Arrange

        // Act
        bool result = str.HasValue();

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void WhenStringNull_WillReturnFalse()
    {
        // Arrange
        string? str = null;

        // Act
        bool result = str.HasValue();

        // Assert
        result.Should().BeFalse();
    }
}
