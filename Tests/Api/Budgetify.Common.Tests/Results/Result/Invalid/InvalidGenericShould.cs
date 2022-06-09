namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(InvalidGenericShould))]
    public class InvalidGenericShould
    {
        [Test]
        public void WillReturnBadRequest()
        {
            // Arrange

            // Act
            Result result = Result.Invalid<string>("BAD_REQUEST");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("BAD_REQUEST");
            result.ResultType.Should().Be(ResultType.BadRequest);
            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.IsNotFound.Should().BeFalse();
        }
    }
}
