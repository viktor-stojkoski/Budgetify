namespace Budgetify.Common.Tests.Extensions.StringExtensions
{
    using Budgetify.Common.Extensions;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(HasValueShould))]
    public class HasValueShould
    {
        [Test]
        public void WhenNonEmptyString_ShouldReturnTrue()
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
        public void WhenWhitespaceAndEmptyString_ShouldReturnFalse(string str)
        {
            // Arrange

            // Act
            bool result = str.HasValue();

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void WhenStringNull_ShouldReturnFalse()
        {
            // Arrange
            string str = null;

            // Act
            bool result = str.HasValue();

            // Assert
            result.Should().BeFalse();
        }
    }
}
