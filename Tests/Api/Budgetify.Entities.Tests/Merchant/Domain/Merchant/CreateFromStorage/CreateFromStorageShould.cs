namespace Budgetify.Entities.Tests.Merchant.Domain.Merchant;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Merchant.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateFromStorageShould))]
public class CreateFromStorageShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        string name = RandomString(256);
        int categoryId = RandomId();

        // Act
        Result<Merchant> result =
            Merchant.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                name: name,
                categoryId: categoryId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.MerchantNameInvalidLength);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateMerchantFromStorage()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2024, 1, 22);
        int userId = RandomId();
        string name = "Test merchant name";
        int categoryId = RandomId();

        // Act
        Result<Merchant> result =
            Merchant.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                name: name,
                categoryId: categoryId);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Unchanged);
        result.Value.Id.Should().Be(id);
        result.Value.Uid.Should().Be(uid);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.UserId.Should().Be(userId);
        result.Value.Name.Value.Should().Be(name);
        result.Value.CategoryId.Should().Be(categoryId);
    }
}
