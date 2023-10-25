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
    /// Returns boolean indicating whether category with the given userId and name exists.
    /// </summary>
    Task<bool> DoesCategoryNameExistAsync(int userId, Guid categoryUid, string? name);

    /// <summary>
    /// Returns boolean indicating whether category with the given userId and categoryUid is valid for deletion.
    /// </summary>
    Task<bool> IsCategoryValidForDeletionAsync(int userId, Guid categoryUid);
}
