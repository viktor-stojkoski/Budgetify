namespace Budgetify.Common.Tests.Results.Result;

using System.Collections.Generic;
using System.Linq;
using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(FirstFailureOrOkGenericShould))]
public class FirstFailureOrOkGenericShould
{
    [Test]
    public void WhenEmptyResults_WillReturnOk()
    {
        // Arrange
        IEnumerable<Result<string>> results = Enumerable.Empty<Result<string>>();

        // Act
        Result result = Result.FirstFailureOrOk(results);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeFalse();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
    }

    [Test]
    public void WhenResultsOk_WillReturnOk()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.Ok("result2");
        Result<string> result3 = Result.Ok("result3");

        IEnumerable<Result<string>> results =
            new List<Result<string>> { result1, result2, result3 };

        // Act
        Result result = Result.FirstFailureOrOk(results);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeFalse();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
    }

    [Test]
    public void WhenOneResultFails_WillReturnFailedResult()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.NotFound<string>("NOT_FOUND");
        Result<string> result3 = Result.Ok("result3");

        IEnumerable<Result<string>> results =
            new List<Result<string>> { result1, result2, result3 };

        // Act
        Result result = Result.FirstFailureOrOk(results);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("NOT_FOUND");
        result.ResultType.Should().Be(ResultType.NotFound);
        result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsNotFound.Should().BeTrue();
    }

    [Test]
    public void WhenMultipleResultsFail_WillReturnFirstFailedResult()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.Conflicted<string>("CONFLICT");
        Result<string> result3 = Result.Ok("result3");
        Result<string> result4 = Result.NotFound<string>("NOT_FOUND");
        Result<string> result5 = Result.Ok("result3");

        IEnumerable<Result<string>> results =
            new List<Result<string>> { result1, result2, result3, result4, result5 };

        // Act
        Result result = Result.FirstFailureOrOk(results);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("CONFLICT");
        result.ResultType.Should().Be(ResultType.Conflict);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Conflict);
        result.IsNotFound.Should().BeFalse();
    }
}
