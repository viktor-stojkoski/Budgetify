namespace Budgetify.Entities.Category.Enumerations;

using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;

public sealed class CategoryType : Enumeration
{
    public static readonly CategoryType Expense = new(1, "EXPENSE");
    public static readonly CategoryType Income = new(1, "INCOME");

    public CategoryType(int id, string name) : base(id, name) { }

    public static Result<CategoryType> Create(string? type)
    {
        CategoryType? categoryType = GetAll<CategoryType>().SingleOrDefault(x => x.Name == type);

        if (categoryType is null)
        {
            return Result.Invalid<CategoryType>(ResultCodes.CategoryTypeInvalid);
        }

        return Result.Ok(categoryType);
    }

    public static implicit operator string(CategoryType type) => type.Name;
}
