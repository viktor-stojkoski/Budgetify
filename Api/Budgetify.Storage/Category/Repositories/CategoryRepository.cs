namespace Budgetify.Storage.Category.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Entities.Category.Domain;
using Budgetify.Storage.Category.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

public class CategoryRepository : Repository<Entities.Category>, ICategoryRepository
{
    public CategoryRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Category category)
    {
        Entities.Category dbCategory = category.CreateCategory();

        Insert(dbCategory);
    }

    public void Update(Category category)
    {
        Entities.Category dbCategory = category.CreateCategory();

        AttachOrUpdate(dbCategory, category.State.GetState());
    }

    public async Task<Result<Category>> GetCategoryAsync(int userId, Guid categoryUid)
    {
        Entities.Category? dbCategory = await AllNoTrackedOf<Entities.Category>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == categoryUid);

        if (dbCategory is null)
        {
            return Result.NotFound<Category>(ResultCodes.CategoryNotFound);
        }

        return dbCategory.CreateCategory();
    }

    public async Task<bool> DoesCategoryNameExistAsync(string? name)
    {
        return await AllNoTrackedOf<Entities.Category>()
            .AnyAsync(x => x.Name == name);
    }
}
