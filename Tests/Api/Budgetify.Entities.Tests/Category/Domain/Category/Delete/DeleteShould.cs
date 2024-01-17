namespace Budgetify.Entities.Tests.Category.Domain.Category;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Domain;
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
        DateTime deletedOn = new(2024, 1, 17);

        Category category = new CategoryBuilder()
            .WithDeletedOn(new DateTime(2024, 1, 15))
            .Build();

        // Act
        Result result = category.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        category.State.Should().Be(EntityState.Unchanged);
        category.DeletedOn.Should().Be(new DateTime(2024, 1, 15));
    }

    [Test]
    public void WhenNotDeleted_WillSetDeletedOn()
    {
        // Arrange
        DateTime deletedOn = new(2024, 1, 17);

        Category category = new CategoryBuilder()
            .Build();

        // Act
        Result result = category.Delete(deletedOn);

        // Assert
        result.IsSuccess.Should().BeTrue();

        category.State.Should().Be(EntityState.Modified);
        category.DeletedOn.Should().Be(deletedOn);
    }
}
