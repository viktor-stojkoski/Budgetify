namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(UnauthorizedShould))]
public class UnauthorizedShould
{
    [Test]
    public void WillReturnUnauthorized()
    {
        // Arrange

        // Act
        Result result = Result.Unauthorized("UNAUTHORIZED");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("UNAUTHORIZED");
        result.ResultType.Should().Be(ResultType.Unauthorized);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.IsNotFound.Should().BeFalse();
    }
}
