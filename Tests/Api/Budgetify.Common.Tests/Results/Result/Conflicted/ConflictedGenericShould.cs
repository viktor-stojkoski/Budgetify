namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(ConflictedGenericShould))]
public class ConflictedGenericShould
{
    [Test]
    public void WillReturnConflicted()
    {
        // Arrange

        // Act
        Result<string> result = Result.Conflicted<string>("CONFLICT");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("CONFLICT");
        result.ResultType.Should().Be(ResultType.Conflict);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Conflict);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().BeNull();
        result.IsEmpty.Should().BeTrue();
    }
}
