namespace Budgetify.Entities.Tests.Transaction.Domain.Transaction;

using System;
using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;
using static DataHelper;

[TestFixture(Category = nameof(CreateFromStorageShould))]
public class CreateFromStorageShould
{
    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
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
        bool isVerified = true;
        IEnumerable<TransactionAttachment> attachments = Enumerable.Empty<TransactionAttachment>();

        // Act
        Result<Transaction> result =
            Transaction.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description,
                isVerified: isVerified,
                attachments: attachments);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionTypeInvalid);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateTransactionFromStorage()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
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
        bool isVerified = true;
        IEnumerable<TransactionAttachment> attachments = CreateTransactionAttachments(id);

        // Act
        Result<Transaction> result =
            Transaction.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                accountId: accountId,
                fromAccountId: fromAccountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: type,
                amount: amount,
                date: date,
                description: description,
                isVerified: isVerified,
                attachments: attachments);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Unchanged);
        result.Value.Id.Should().Be(id);
        result.Value.Uid.Should().Be(uid);
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
        result.Value.IsVerified.Should().Be(isVerified);
        result.Value.Attachments.Should().BeEquivalentTo(attachments);
    }
}
