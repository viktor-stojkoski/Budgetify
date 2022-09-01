namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(FirstFailureNullOrOkShould))]
public class FirstFailureNullOrOkShould
{
    [Test]
    public void WhenResultsOk_WillReturnOk()
    {
        // Arrange
        Result result1 = Result.Ok();
        Result result2 = Result.Ok();
        Result result3 = Result.Ok();

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3);

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
        Result result1 = Result.Ok();
        Result result2 = Result.NotFound("NOT_FOUND");
        Result result3 = Result.Ok();

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3);

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
        Result result1 = Result.Ok();
        Result result2 = Result.Conflicted("CONFLICT");
        Result result3 = Result.Ok();
        Result result4 = Result.NotFound("NOT_FOUND");
        Result result5 = Result.Ok();

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3, result4, result5);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("CONFLICT");
        result.ResultType.Should().Be(ResultType.Conflict);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Conflict);
        result.IsNotFound.Should().BeFalse();
    }

    [Test]
    public void WhenGenericResultsAreOk_WillReturnOk()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.Ok("result2");
        Result<string> result3 = Result.Ok("result3");

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3);

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
    public void WhenOneGenericResultFails_WillReturnFailedResult()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.NotFound<string>("NOT_FOUND");
        Result<string> result3 = Result.Ok("result3");

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3);

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
    public void WhenMultipleGenericResultsFail_WillReturnFirstFailedResult()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string> result2 = Result.Conflicted<string>("CONFLICT");
        Result<string> result3 = Result.Ok("result3");
        Result<string> result4 = Result.NotFound<string>("NOT_FOUND");
        Result<string> result5 = Result.Ok("result3");

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3, result4, result5);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("CONFLICT");
        result.ResultType.Should().Be(ResultType.Conflict);
        result.HttpStatusCode.Should().Be(HttpStatusCode.Conflict);
        result.IsNotFound.Should().BeFalse();
    }

    [Test]
    public void WhenGenericResultsHaveOneNullValue_WillReturnOkWithFailureOrNullTrue()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string?> result2 = Result.Ok<string?>(null);
        Result<string> result3 = Result.Ok("result3");

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
    }

    [Test]
    public void WhenGenericResultsHaveMultipleNullValues_WillReturnOkWithFailureOrNullTrue()
    {
        // Arrange
        Result<string> result1 = Result.Ok("result1");
        Result<string?> result2 = Result.Ok<string?>(null);
        Result<string> result3 = Result.Ok("result3");
        Result<string?> result4 = Result.Ok<string?>(null);
        Result<string> result5 = Result.Ok("result3");

        // Act
        Result result = Result.FirstFailureNullOrOk(result1, result2, result3, result4, result5);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
    }
}
