namespace Budgetify.Common.Tests.Results.Result.FirstFailureNullOrOk
{
    using System.Net;

    using Budgetify.Common.Results;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture(Category = nameof(FirstFailureOrOkGenericShould))]
    public class FirstFailureNullOrOkShould
    {
        [Test]
        public void WhenResultsOk_WillReturnOk()
        {
            // Arrange
            Result<string?> result1 = Result.Ok<string?>(null);
            Result result2 = Result.Ok();
            Result result3 = Result.Ok();

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
    }
}
