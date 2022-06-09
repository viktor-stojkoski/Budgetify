namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(FailedGenericShould))]
    public class FailedGenericShould
    {
        [Test]
        public void WillReturnInternalServerError()
        {
            // Arrange

            // Act
            Result<string> result = Result.Failed<string>("INTERNAL_SERVER_ERROR");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("INTERNAL_SERVER_ERROR");
            result.ResultType.Should().Be(ResultType.InternalServerError);
            result.HttpStatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.IsNotFound.Should().BeFalse();
            result.Value.Should().BeNull();
            result.IsEmpty.Should().BeTrue();
        }
    }
}
