namespace Budgetify.Contracts.Category.Repositories;

using Budgetify.Entities.Category.Domain;

public interface ICategoryRepository
{
    /// <summary>
    /// Inserts new category.
    /// </summary>
    void Insert(Category category);

    /// <summary>
    /// Updates category.
    /// </summary>
    void Update(Category category);
}
