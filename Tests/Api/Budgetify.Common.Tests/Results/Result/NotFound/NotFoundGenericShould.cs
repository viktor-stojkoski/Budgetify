﻿namespace Budgetify.Common.Tests.Results.Result;

using System.Net;

using Budgetify.Common.Results;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(NotFoundGenericShould))]
public class NotFoundGenericShould
{
    [Test]
    public void WillReturnNotFound()
    {
        // Arrange

        // Act
        Result<string> result = Result.NotFound<string>("NOT_FOUND");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailureOrNull.Should().BeTrue();
        result.Message.Should().Be("NOT_FOUND");
        result.ResultType.Should().Be(ResultType.NotFound);
        result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsNotFound.Should().BeTrue();
        result.Value.Should().BeNull();
        result.IsEmpty.Should().BeTrue();
    }
}
