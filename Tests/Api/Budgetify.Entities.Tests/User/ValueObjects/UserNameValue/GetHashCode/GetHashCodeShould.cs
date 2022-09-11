namespace Budgetify.Entities.Tests.User.ValueObjects.UserNameValue.GetHashCode;

using Budgetify.Common.Results;
using Budgetify.Entities.User.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(GetHashCodeShould))]
public class GetHashCodeShould
{
    [Test]
    public void WhenCorrect_WillReturnHashCode()
    {
        // Arrange
        Result<UserNameValue> value = UserNameValue.Create("Viktor");

        // Act
        int result = value.Value.GetHashCode();

        // Assert
        object obj = result;
        result.Should().Be(obj.GetHashCode());
    }
}
