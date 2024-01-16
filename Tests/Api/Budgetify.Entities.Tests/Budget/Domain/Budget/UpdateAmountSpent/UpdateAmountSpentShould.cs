namespace Budgetify.Entities.Tests.Budget.Domain.Budget;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(UpdateAmountSpentShould))]
public class UpdateAmountSpentShould
{
    [Test]
    public void WhenAmountSpentUpdated_WillUpdateAmountSpent()
    {
        // Arrange
        Budget budget = new BudgetBuilder()
            .Build();

        // Act
        Result result = budget.UpdateAmountSpent(2000);

        // Assert
        result.IsSuccess.Should().BeTrue();

        budget.State.Should().Be(EntityState.Modified);
        budget.AmountSpent.Should().Be(3000);
    }
}
