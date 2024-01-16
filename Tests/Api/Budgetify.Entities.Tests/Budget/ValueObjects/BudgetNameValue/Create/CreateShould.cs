namespace Budgetify.Entities.Tests.Budget.ValueObjects.BudgetNameValue;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.ValueObjects;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<BudgetNameValue> result = BudgetNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetNameInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<BudgetNameValue> result = BudgetNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetNameInvalid);
    }

    [Test]
    public void WhenValueHasMoreThan255Characters_WillReturnErrorResult()
    {
        // Arrange
        string value = RandomString(256);

        // Act
        Result<BudgetNameValue> result = BudgetNameValue.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetNameInvalidLength);
    }

    [Test]
    public void WhenValueCorrect_WillCreateBudgetName()
    {
        // Arrange
        string value = "Test Budget Name";

        // Act
        Result<BudgetNameValue> result = BudgetNameValue.Create(value);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(value);
    }
}
