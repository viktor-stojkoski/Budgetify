namespace Budgetify.Contracts.Category.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
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

    /// <summary>
    /// Returns category by given userId and categoryUid.
    /// </summary>
    Task<Result<Category>> GetCategoryAsync(int userId, Guid categoryUid);

    /// <summary>
    /// Returns boolean indicating whether category with the given name exists.
    /// </summary>
    Task<bool> DoesCategoryNameExistAsync(string? name);
}
