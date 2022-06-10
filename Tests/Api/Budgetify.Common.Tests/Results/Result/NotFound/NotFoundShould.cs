namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(NotFoundShould))]
    public class NotFoundShould
    {
        [Test]
        public void WillReturnNotFound()
        {
            // Arrange

            // Act
            Result result = Result.NotFound("NOT_FOUND");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("NOT_FOUND");
            result.ResultType.Should().Be(ResultType.NotFound);
            result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
            result.IsNotFound.Should().BeTrue();
        }
    }
}
