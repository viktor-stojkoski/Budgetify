namespace Budgetify.Common.Tests.Results.Result
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(OkShould))]
    public class OkShould
    {
        [Test]
        public void WillReturnOk()
        {
            // Arrange

            // Act
            Result result = Result.Ok();

            // Assert
            result.IsFailure.Should().BeFalse();
            result.IsSuccess.Should().BeTrue();
            result.IsFailureOrNull.Should().BeFalse();
            result.Message.Should().BeEmpty();
            result.ResultType.Should().Be(ResultType.Ok);
            result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            result.IsNotFound.Should().BeFalse();
        }
    }
}
