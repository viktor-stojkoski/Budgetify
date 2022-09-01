namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(UnauthorizedGenericShould))]
public class UnauthorizedGenericShould
{
    [Test]
    public void WillReturnUnauthorized()
    {
        // Arrange

        // Act
        Result<string> result = Result.Unauthorized<string>("UNAUTHORIZED");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("UNAUTHORIZED");
        result.ResultType.Should().Be(ResultType.Unauthorized);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().BeNull();
        result.IsEmpty.Should().BeTrue();
    }
}
