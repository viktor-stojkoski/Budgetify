namespace Budgetify.Entities.Tests.Account.Domain.Account;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Common.Enumerations;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(DeductBalanceShould))]
public class DeductBalanceShould
{
    [Test]
    public void WhenBalanceUpdated_WillDeductBalance()
    {
        // Arrange
        Account account = new AccountBuilder()
            .Build();

        // Act
        Result result = account.DeductBalance(1000);

        // Assert
        result.IsSuccess.Should().BeTrue();

        account.State.Should().Be(EntityState.Modified);
        account.Balance.Should().Be(9000);
    }
}
