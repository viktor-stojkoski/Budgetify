namespace Budgetify.Common.Tests.Results.Result
{
    using System;
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(FromErrorShould))]
    public class FromErrorShould
    {
        [Test]
        public void WhenResultOk_WillThrowException()
        {
            // Arrange
            Result result1 = Result.Ok();

            // Act
            Action action = () => Result.FromError<string>(result1);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .And
                .Message.Should().Be("There must be error message for failure. (Parameter 'message')"); ;
        }

        [Test]
        public void WhenResultInvalid_WillReturnInvalidResult()
        {
            // Arrange
            Result result1 = Result.Invalid("INVALID");

            // Act
            Result<string> result = Result.FromError<string>(result1);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("INVALID");
            result.ResultType.Should().Be(ResultType.BadRequest);
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.IsNotFound.Should().BeFalse();
            result.Value.Should().BeNull();
            result.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void WhenMultipleResultsFailed_WillReturnFailedResult()
        {
            // Arrange
            Result result1 = Result.Invalid("INVALID");
            Result<string> result2 = Result.Ok("result2");
            Result<string> result3 = Result.Ok("result3");
            Result<string> result4 = Result.NotFound<string>("NOT_FOUND");

            Result firstFailureOrOk = Result.FirstFailureOrOk(result1, result2, result3, result4);

            // Act
            Result<string> result = Result.FromError<string>(firstFailureOrOk);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("INVALID");
            result.ResultType.Should().Be(ResultType.BadRequest);
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.IsNotFound.Should().BeFalse();
            result.Value.Should().BeNull();
            result.IsEmpty.Should().BeTrue();
        }
    }
}
