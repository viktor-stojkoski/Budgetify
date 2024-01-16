namespace Budgetify.Entities.Tests.Budget.Domain.Budget;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 16);
        int userId = RandomId();
        string name = RandomString(256);
        int categoryId = RandomId();
        int currencyId = RandomId();
        DateTime startDate = new(2024, 1, 1);
        DateTime endDate = new(2024, 2, 1);
        decimal amount = 10000;
        decimal amountSpent = 2000;

        // Act
        Result<Budget> result =
            Budget.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                categoryId: categoryId,
                currencyId: currencyId,
                startDate: startDate,
                endDate: endDate,
                amount: amount,
                amountSpent: amountSpent);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetNameInvalidLength);
    }

    [Test]
    public void WhenDateRangeInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 16);
        int userId = RandomId();
        string name = "Name";
        int categoryId = RandomId();
        int currencyId = RandomId();
        DateTime startDate = new(2024, 2, 1);
        DateTime endDate = new(2024, 1, 1);
        decimal amount = 10000;
        decimal amountSpent = 2000;

        // Act
        Result<Budget> result =
            Budget.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                categoryId: categoryId,
                currencyId: currencyId,
                startDate: startDate,
                endDate: endDate,
                amount: amount,
                amountSpent: amountSpent);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.BudgetStartDateCannotBeGreaterThanEndDate);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateBudget()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 16);
        int userId = RandomId();
        string name = "Name";
        int categoryId = RandomId();
        int currencyId = RandomId();
        DateTime startDate = new(2024, 1, 1);
        DateTime endDate = new(2024, 2, 1);
        decimal amount = 10000;
        decimal amountSpent = 2000;

        // Act
        Result<Budget> result =
            Budget.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                categoryId: categoryId,
                currencyId: currencyId,
                startDate: startDate,
                endDate: endDate,
                amount: amount,
                amountSpent: amountSpent);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Added);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.UserId.Should().Be(userId);
        result.Value.Name.Value.Should().Be(name);
        result.Value.CategoryId.Should().Be(categoryId);
        result.Value.CurrencyId.Should().Be(currencyId);
        result.Value.DateRange.StartDate.Should().Be(startDate);
        result.Value.DateRange.EndDate.Should().Be(endDate);
        result.Value.Amount.Should().Be(amount);
        result.Value.AmountSpent.Should().Be(amountSpent);
    }
}
