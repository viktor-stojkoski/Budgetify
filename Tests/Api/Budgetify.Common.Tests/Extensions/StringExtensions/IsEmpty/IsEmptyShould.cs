namespace Budgetify.Common.Tests.Extensions.StringExtensions
{
    using Budgetify.Common.Extensions;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(IsEmptyShould))]
    public class IsEmptyShould
    {
        [Test]
        public void WhenNonEmptyString_WillReturnFalse()
        {
            // Arrange
            string str = "Test string";

            // Act
            bool result = str.IsEmpty();

            // Assert
            result.Should().BeFalse();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        public void WhenWhitespaceAndEmptyString_WillReturnTrue(string str)
        {
            // Arrange

            // Act
            bool result = str.IsEmpty();

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void WhenStringNull_WillReturnTrue()
        {
            // Arrange
            string? str = null;

            // Act
            bool result = str.IsEmpty();

            // Assert
            result.Should().BeTrue();
        }
    }
}
