namespace Budgetify.Entities.Tests.Account.Enumerations.AccountType;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(CreateShould))]
public class CreateShould
{
    [Test]
    public void WhenValueNull_WillReturnErrorResult()
    {
        // Arrange
        string? value = null;

        // Act
        Result<AccountType> result = AccountType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountTypeInvalid);
    }

    [Test]
    public void WhenValueEmptyString_WillReturnErrorResult()
    {
        // Arrange
        string value = "";

        // Act
        Result<AccountType> result = AccountType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountTypeInvalid);
    }

    [Test]
    public void WhenValueDoesNotExist_WillReturnErrorResult()
    {
        // Arrange
        string value = "RANDOM_VALUE";

        // Act
        Result<AccountType> result = AccountType.Create(value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.AccountTypeInvalid);
    }

    [Test]
    [TestCase(1, "CASH")]
    [TestCase(2, "DEBIT")]
    [TestCase(3, "CREDIT")]
    [TestCase(4, "SAVINGS")]
    public void WhenValueCorrect_WillCreateAccountType(int id, string name)
    {
        // Arrange

        // Act
        Result<AccountType> result = AccountType.Create(name);

        // Assert
        result.IsSuccess.Should().BeTrue();

        string implicitOperatorResult = result.Value;
        implicitOperatorResult.Should().Be(name);
        result.Value.Id.Should().Be(id);
    }
}
