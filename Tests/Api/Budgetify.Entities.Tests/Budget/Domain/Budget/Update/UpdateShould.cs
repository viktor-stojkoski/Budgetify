namespace Budgetify.Entities.Tests.Budget.Domain.Budget;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(UpdateShould))]
public class UpdateShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        string name = "";
        decimal amount = 1000;

        Budget budget = new BudgetBuilder()
            .Build();

        // Act
        Result result = budget.Update(name, amount);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetNameInvalid);

        budget.State.Should().Be(EntityState.Unchanged);
        budget.Name.Value.Should().Be("Name");
        budget.Amount.Should().Be(10000);
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateBudget()
    {
        // Arrange
        string name = "New Name";
        decimal amount = 1000;

        Budget budget = new BudgetBuilder()
            .Build();

        // Act
        Result result = budget.Update(name, amount);

        // Assert
        result.IsSuccess.Should().BeTrue();

        budget.State.Should().Be(EntityState.Modified);
        budget.Name.Value.Should().Be(name);
        budget.Amount.Should().Be(1000);
    }
}
