namespace Budgetify.Entities.Tests.Category.Domain.Category;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Domain;
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
        string type = "INCOME";

        Category category = new CategoryBuilder()
            .Build();

        // Act
        Result result = category.Update(name, type);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryNameInvalidLength);

        category.State.Should().Be(EntityState.Unchanged);
        category.Name.Value.Should().Be("Name");
        category.Type.Name.Should().Be("EXPENSE");
    }

    [Test]
    public void WhenTypeInvalid_WillReturnErrorResult()
    {
        // Arrange
        string name = "New Name";
        string type = "INVALID";

        Category category = new CategoryBuilder()
            .Build();

        // Act
        Result result = category.Update(name, type);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CategoryTypeInvalid);

        category.State.Should().Be(EntityState.Unchanged);
        category.Name.Value.Should().Be("Name");
        category.Type.Name.Should().Be("EXPENSE");
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateCategory()
    {
        // Arrange
        string name = "New Name";
        string type = "INCOME";

        Category category = new CategoryBuilder()
            .Build();

        // Act
        Result result = category.Update(name, type);

        // Assert
        result.IsSuccess.Should().BeTrue();

        category.State.Should().Be(EntityState.Modified);
        category.Name.Value.Should().Be(name);
        category.Type.Name.Should().Be(type);
    }
}
