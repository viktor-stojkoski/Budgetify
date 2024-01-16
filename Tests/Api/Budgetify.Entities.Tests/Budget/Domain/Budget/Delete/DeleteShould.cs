namespace Budgetify.Entities.Tests.Budget.Domain.Budget;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(DeleteShould))]
public class DeleteShould
{
    [Test]
    public void WhenAlreadyDeleted_WontOverrideDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 16);

        Budget budget = new BudgetBuilder()
            .WithDeletedOn(new DateTime(2024, 1, 15))
            .Build();

        // Act
        Result result = budget.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        budget.State.Should().Be(EntityState.Unchanged);
        budget.DeletedOn.Should().Be(new DateTime(2024, 1, 15));
    }

    [Test]
    public void WhenNotDeleted_WillSetDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 16);

        Budget budget = new BudgetBuilder()
            .Build();

        // Act
        Result result = budget.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        budget.State.Should().Be(EntityState.Modified);
        budget.DeletedOn.Should().Be(deletedOn);
    }
}
