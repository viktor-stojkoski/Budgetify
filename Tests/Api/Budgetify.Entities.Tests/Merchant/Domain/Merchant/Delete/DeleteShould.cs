namespace Budgetify.Entities.Tests.Merchant.Domain.Merchant;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Merchant.Domain;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(DeleteShould))]
public class DeleteShould
{
    [Test]
    public void WhenAlreadyDeleted_WontOverrideDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 22);

        Merchant merchant = new MerchantBuilder()
            .WithDeletedOn(new DateTime(2024, 1, 20))
            .Build();

        // Act
        Result result = merchant.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        merchant.State.Should().Be(EntityState.Unchanged);
        merchant.DeletedOn.Should().Be(new DateTime(2024, 1, 20));
    }

    [Test]
    public void WhenNotDeleted_WillSetDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 22);

        Merchant merchant = new MerchantBuilder()
            .Build();

        // Act
        Result result = merchant.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        merchant.State.Should().Be(EntityState.Modified);
        merchant.DeletedOn.Should().Be(deletedOn);
    }
}
