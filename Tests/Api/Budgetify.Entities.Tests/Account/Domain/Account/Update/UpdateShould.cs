namespace Budgetify.Entities.Tests.Account.Domain.Account;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(UpdateShould))]
public class UpdateShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        string name = RandomString(256);
        string type = "DEBIT";
        decimal balance = 20000;
        int currencyId = RandomId();
        string description = "Test description";

        Account account = new AccountBuilder()
            .Build();

        // Act
        Result result = account.Update(
            name: name,
            type: type,
            balance: balance,
            currencyId: currencyId,
            description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountNameInvalidLength);

        account.State.Should().Be(EntityState.Unchanged);
        account.Name.Value.Should().Be("Name");
        account.Type.Name.Should().Be("CASH");
        account.Balance.Should().Be(10000);
        account.CurrencyId.Should().Be(3);
        account.Description.Should().Be("Description");
    }

    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        string name = "Test account name";
        string type = "INVALID";
        decimal balance = 20000;
        int currencyId = RandomId();
        string description = "Test description";

        Account account = new AccountBuilder()
            .Build();

        // Act
        Result result = account.Update(
            name: name,
            type: type,
            balance: balance,
            currencyId: currencyId,
            description: description);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountTypeInvalid);

        account.State.Should().Be(EntityState.Unchanged);
        account.Name.Value.Should().Be("Name");
        account.Type.Name.Should().Be("CASH");
        account.Balance.Should().Be(10000);
        account.CurrencyId.Should().Be(3);
        account.Description.Should().Be("Description");
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateAccount()
    {
        // Arrange
        string name = "Test name";
        string type = "DEBIT";
        decimal balance = 20000;
        int currencyId = RandomId();
        string description = "Test description";

        Account account = new AccountBuilder()
            .Build();

        // Act
        Result result = account.Update(
            name: name,
            type: type,
            balance: balance,
            currencyId: currencyId,
            description: description);

        // Assert
        result.IsSuccess.Should().BeTrue();

        account.State.Should().Be(EntityState.Modified);
        account.Name.Value.Should().Be(name);
        account.Type.Name.Should().Be(type);
        account.Balance.Should().Be(balance);
        account.CurrencyId.Should().Be(currencyId);
        account.Description.Should().Be(description);
    }
}
