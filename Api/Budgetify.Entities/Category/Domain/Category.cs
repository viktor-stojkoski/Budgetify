namespace Budgetify.Entities.Category.Domain;

using Budgetify.Entities.Category.Enumerations;
using Budgetify.Entities.Category.ValueObjects;
using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;

public sealed partial class Category : AggregateRoot
{
    public Category(
        int userId,
        CategoryNameValue name,
        CategoryType type)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        Name = name;
        Type = type;
    }

    /// <summary>
    /// User that owns this category.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Category's name.
    /// </summary>
    public CategoryNameValue Name { get; private set; }

    /// <summary>
    /// Category's type.
    /// </summary>
    public CategoryType Type { get; private set; }
}
