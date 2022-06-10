namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(ForbiddenShould))]
    public class ForbiddenShould
    {
        [Test]
        public void WillReturnForbidden()
        {
            // Arrange

            // Act
            Result result = Result.Forbidden("FORBIDDEN");

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
            result.IsFailureOrNull.Should().BeTrue();
            result.Message.Should().Be("FORBIDDEN");
            result.ResultType.Should().Be(ResultType.Forbidden);
            result.HttpStatusCode.Should().Be(HttpStatusCode.Forbidden);
            result.IsNotFound.Should().BeFalse();
        }
    }
}
