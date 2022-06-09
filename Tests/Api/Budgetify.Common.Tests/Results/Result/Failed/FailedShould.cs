namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(FailedShould))]
    public class FailedShould
    {
        [Test]
        public void WillReturnInternalServerError()
        {
            // Arrange

            // Act
            Result result = Result.Failed("INTERNAL_SERVER_ERROR");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("INTERNAL_SERVER_ERROR");
            result.ResultType.Should().Be(ResultType.InternalServerError);
            result.HttpStatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.IsNotFound.Should().BeFalse();
        }
    }
}
