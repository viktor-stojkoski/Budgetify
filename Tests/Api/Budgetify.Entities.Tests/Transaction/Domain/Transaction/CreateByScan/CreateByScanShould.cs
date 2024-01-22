namespace Budgetify.Entities.Tests.Transaction.Domain.Transaction;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateByScanShould))]
public class CreateByScanShould
{
    [Test]
    public void WhenArgumentsCorrect_WillCreateTransactionByScan()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int currencyId = RandomId();
        int merchantId = RandomId();
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);

        // Act
        Result<Transaction> result =
            Transaction.CreateByScan(
                createdOn: createdOn,
                userId: userId,
                currencyId: currencyId,
                merchantId: merchantId,
                amount: amount,
                date: date);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Added);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.UserId.Should().Be(userId);
        result.Value.AccountId.Should().BeNull();
        result.Value.FromAccountId.Should().BeNull();
        result.Value.CategoryId.Should().BeNull();
        result.Value.CurrencyId.Should().Be(currencyId);
        result.Value.MerchantId.Should().Be(merchantId);
        result.Value.Type.Name.Should().Be("EXPENSE");
        result.Value.Amount.Should().Be(amount);
        result.Value.Date.Should().Be(date);
        result.Value.Description.Should().BeNull();
        result.Value.IsVerified.Should().BeFalse();
        result.Value.Attachments.Should().BeEmpty();
    }
}
