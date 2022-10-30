namespace Budgetify.Entities.Category.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Enumerations;
using Budgetify.Entities.Category.ValueObjects;
using Budgetify.Entities.Common.Enumerations;

public partial class Category
{
    /// <summary>
    /// Create category DB to domain only.
    /// </summary>
    public static Result<Category> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        string type)
    {
        Result<CategoryNameValue> nameValue = CategoryNameValue.Create(name);
        Result<CategoryType> typeValue = CategoryType.Create(type);

        Result okOrError = Result.FirstFailureNullOrOk(nameValue, typeValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<Category>(okOrError);
        }

        return Result.Ok(
            new Category(
                userId: userId,
                name: nameValue.Value,
                type: typeValue.Value)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates category.
    /// </summary>
    public static Result<Category> Create(
        DateTime createdOn,
        int userId,
        string? name,
        string? type)
    {
        Result<CategoryNameValue> nameValue = CategoryNameValue.Create(name);
        Result<CategoryType> typeValue = CategoryType.Create(type);

        Result okOrError = Result.FirstFailureNullOrOk(nameValue, typeValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<Category>(okOrError);
        }

        return Result.Ok(
            new Category(
                userId: userId,
                name: nameValue.Value,
                type: typeValue.Value)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
