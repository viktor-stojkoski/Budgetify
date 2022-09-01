namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(OkGenericShould))]
public class OkGenericShould
{
    [Test]
    public void WhenHasValue_WillReturnOk()
    {
        // Arrange
        string value = "Test string";

        // Act
        Result<string> result = Result.Ok(value);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeFalse();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().Be(value);
        result.IsEmpty.Should().BeFalse();
    }

    [Test]
    public void WhenValueIsEmpty_WillReturnOk()
    {
        // Arrange
        string value = string.Empty;

        // Act
        Result<string> result = Result.Ok(value);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeFalse();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().Be(value);
        result.IsEmpty.Should().BeFalse();
    }

    [Test]
    public void WhenValueIsNull_WillReturnOk()
    {
        // Arrange
        string? value = null;

        // Act
        Result<string?> result = Result.Ok(value);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.ResultType.Should().Be(ResultType.Ok);
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        result.IsNotFound.Should().BeFalse();
        result.Value.Should().Be(value);
        result.IsEmpty.Should().BeTrue();
    }
}
