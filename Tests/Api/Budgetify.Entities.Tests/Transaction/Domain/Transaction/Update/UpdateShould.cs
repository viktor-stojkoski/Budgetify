namespace Budgetify.Entities.Tests.Transaction.Domain.Transaction;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(UpdateShould))]
public class UpdateShould
{
    [Test]
    public void WhenTypeExpenseAndMerchantIdNotPresent_WillReturnErrorResult()
    {
        // Arrange
        int accountId = RandomId();
        int fromAccountId = RandomId();
        int categoryId = RandomId();
        int currencyId = RandomId();
        int? merchantId = null;
        decimal amount = 2000;
        DateTime date = new(2024, 1, 31);
        string? description = null;

        Transaction transaction = new TransactionBuilder()
            .WithType()
            .Build();

        // Act
        Result result = transaction.Update(
            accountId: accountId,
            fromAccountId: fromAccountId,
            categoryId: categoryId,
            currencyId: currencyId,
            merchantId: merchantId,
            amount: amount,
            date: date,
            description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.TransactionEmptyMerchantTypeInvalid);
    }
}
