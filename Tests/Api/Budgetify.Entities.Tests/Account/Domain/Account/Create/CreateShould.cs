namespace Budgetify.Entities.Tests.Account.Domain.Account;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;
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
        DateTime createdOn = new(2022, 9, 9);
        int userId = RandomId();
        string name = RandomString(256);
        string type = "CASH";
        decimal balance = 1000;
        int currencyId = RandomId();
        string? description = "Test description";

        // Act
        Result<Account> result =
            Account.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type,
                balance: balance,
                currencyId: currencyId,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountNameInvalidLength);
    }

    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        DateTime createdOn = new(2022, 9, 9);
        int userId = RandomId();
        string name = "Test account name";
        string type = "INVALID";
        decimal balance = 1000;
        int currencyId = RandomId();
        string? description = "Test description";

        // Act
        Result<Account> result =
            Account.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type,
                balance: balance,
                currencyId: currencyId,
                description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountTypeInvalid);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateAccountFromStorage()
    {
        // Arrange
        DateTime createdOn = new(2022, 9, 9);
        int userId = RandomId();
        string name = "Test account name";
        string type = "CASH";
        decimal balance = 1000;
        int currencyId = RandomId();
        string? description = "Test description";

        // Act
        Result<Account> result =
            Account.Create(
                createdOn: createdOn,
                userId: userId,
                name: name,
                type: type,
                balance: balance,
                currencyId: currencyId,
                description: description);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Added);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.Name.Value.Should().Be(name);
        result.Value.Type.Name.Should().Be(type);
        result.Value.Balance.Should().Be(balance);
        result.Value.CurrencyId.Should().Be(currencyId);
        result.Value.Description.Should().Be(description);
    }
}
