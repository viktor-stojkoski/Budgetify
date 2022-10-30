namespace Budgetify.Storage.Category.Repositories;

using Budgetify.Contracts.Category.Repositories;
using Budgetify.Entities.Category.Domain;
using Budgetify.Storage.Category.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

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
}
