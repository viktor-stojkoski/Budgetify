namespace Budgetify.Storage.Category.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Domain;

internal static class CategoryFactory
{
    /// <summary>
    /// Creates <see cref="Category"/> domain entity for a given <see cref="Entities.Category"/> storage entity.
    /// </summary>
    internal static Result<Category> CreateCategory(this Entities.Category dbCategory)
    {
        return Category.Create(
            id: dbCategory.Id,
            uid: dbCategory.Uid,
            createdOn: dbCategory.CreatedOn,
            deletedOn: dbCategory.DeletedOn,
            userId: dbCategory.UserId,
            name: dbCategory.Name,
            type: dbCategory.Type);
    }

    /// <summary>
    /// Creates <see cref="Entities.Category"/> storage entity for a given <see cref="Category"/> domain entity.
    /// </summary>
    internal static Entities.Category CreateCategory(this Category category)
    {
        return new(
            id: category.Id,
            uid: category.Uid,
            createdOn: category.CreatedOn,
            deletedOn: category.DeletedOn,
            userId: category.UserId,
            name: category.Name,
            type: category.Type);
    }
}
