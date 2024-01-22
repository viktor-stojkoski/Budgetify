namespace Budgetify.Entities.Tests.Transaction.Domain.Transaction;

using System;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Entities.Transaction.DomainEvents;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int categoryId = RandomId();
        int currencyId = RandomId();
        int merchantId = RandomId();
        string type = "INVALID";
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);
        string description = "Test description";

        // Act
        Result<Transaction> result =
            Transaction.Create(
                createdOn: createdOn,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionTypeInvalid);
    }

    [Test]
    public void WhenTypeExpenseAndMerchantNotPresent_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int categoryId = RandomId();
        int currencyId = RandomId();
        int? merchantId = null;
        string type = "EXPENSE";
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);
        string description = "Test description";

        // Act
        Result<Transaction> result =
            Transaction.Create(
                createdOn: createdOn,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionEmptyMerchantTypeInvalid);
    }

    [Test]
    public void WhenTypeNotExpenseAndMerchantPresent_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int categoryId = RandomId();
        int currencyId = RandomId();
        int merchantId = RandomId();
        string type = "INCOME";
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);
        string description = "Test description";

        // Act
        Result<Transaction> result =
            Transaction.Create(
                createdOn: createdOn,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionTypeNotCompatibleWithMerchant);
    }

    [Test]
    public void WhenTypeNotTransferAndCategoryNotPresent_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int? categoryId = null;
        int currencyId = RandomId();
        int merchantId = RandomId();
        string type = "EXPENSE";
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);
        string description = "Test description";

        // Act
        Result<Transaction> result =
            Transaction.Create(
                createdOn: createdOn,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionCategoryMissing);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateTransaction()
    {
        // Arrange
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int categoryId = RandomId();
        int currencyId = RandomId();
        int merchantId = RandomId();
        string type = "EXPENSE";
        decimal amount = 10000;
        DateTime date = new(2024, 1, 20);
        string description = "Test description";

        // Act
        Result<Transaction> result =
            Transaction.Create(
                createdOn: createdOn,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Added);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.UserId.Should().Be(userId);
        result.Value.AccountId.Should().Be(accountId);
        result.Value.FromAccountId.Should().Be(fromAccountId);
        result.Value.CategoryId.Should().Be(categoryId);
        result.Value.CurrencyId.Should().Be(currencyId);
        result.Value.MerchantId.Should().Be(merchantId);
        result.Value.Type.Name.Should().Be(type);
        result.Value.Amount.Should().Be(amount);
        result.Value.Date.Should().Be(date);
        result.Value.Description.Should().Be(description);
        result.Value.IsVerified.Should().BeTrue();
        result.Value.Attachments.Should().BeEmpty();

        result.Value.DomainEvents.Should().HaveCount(1);

        TransactionCreatedDomainEvent? domainEvent =
            result.Value.DomainEvents
                .SingleOrDefault(x => x is TransactionCreatedDomainEvent)
                    as TransactionCreatedDomainEvent;

        domainEvent.Should().NotBeNull();
        domainEvent?.UserId.Should().Be(userId);
        domainEvent?.TransactionUid.Should().Be(result.Value.Uid);
        domainEvent?.TransactionType.Name.Should().Be(type);
    }
}
