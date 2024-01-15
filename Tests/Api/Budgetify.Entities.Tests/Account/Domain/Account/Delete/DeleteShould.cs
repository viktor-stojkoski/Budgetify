namespace Budgetify.Entities.Tests.Account.Domain.Account;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Domain;
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
        DateTime deletedOn = new(2024, 1, 13);

        Account account = new AccountBuilder()
            .WithDeletedOn(new DateTime(2024, 1, 15))
            .Build();

        // Act
        Result result = account.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        account.State.Should().Be(EntityState.Unchanged);
        account.DeletedOn.Should().Be(new DateTime(2024, 1, 15));
    }

    [Test]
    public void WhenNotDeleted_WillSetDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 13);

        Account account = new AccountBuilder()
            .Build();

        // Act
        Result result = account.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        account.State.Should().Be(EntityState.Modified);
        account.DeletedOn.Should().Be(deletedOn);
    }
}
