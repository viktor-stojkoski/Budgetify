namespace Budgetify.Entities.Tests.Merchant.Domain.Merchant;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Merchant.Domain;

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
        int categoryId = RandomId();

        Merchant merchant = new MerchantBuilder()
            .Build();

        // Act
        Result result = merchant.Update(name, categoryId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.MerchantNameInvalidLength);

        merchant.State.Should().Be(EntityState.Unchanged);
        merchant.Name.Value.Should().Be("Name");
        merchant.CategoryId.Should().Be(3);
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateMerchant()
    {
        // Arrange
        string name = "Test name";
        int categoryId = RandomId();

        Merchant merchant = new MerchantBuilder()
            .Build();

        // Act
        Result result = merchant.Update(name, categoryId);

        // Assert
        result.IsSuccess.Should().BeTrue();

        merchant.State.Should().Be(EntityState.Modified);
        merchant.Name.Value.Should().Be(name);
        merchant.CategoryId.Should().Be(categoryId);
    }
}
