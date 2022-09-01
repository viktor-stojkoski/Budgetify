namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(ForbiddenGenericShould))]
public class ForbiddenGenericShould
{
    [Test]
    public void WillReturnForbidden()
    {
        // Arrange

        // Act
        Result<string> result = Result.Forbidden<string>("FORBIDDEN");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("FORBIDDEN");
        result.ResultType.Should().Be(ResultType.Forbidden);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().BeNull();
        result.IsEmpty.Should().BeTrue();
    }
}
