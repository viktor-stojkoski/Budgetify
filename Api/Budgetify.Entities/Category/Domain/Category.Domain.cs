namespace Budgetify.Entities.Category.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Category.Enumerations;
using Budgetify.Entities.Category.ValueObjects;

public partial class Category
{
    /// <summary>
    /// Updates category.
    /// </summary>
    public Result Update(string? name, string? type)
    {
        Result<CategoryNameValue> nameValue = CategoryNameValue.Create(name);
        Result<CategoryType> typeValue = CategoryType.Create(type);

        Result okOrError = Result.FirstFailureNullOrOk(nameValue, typeValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<Category>(okOrError);
        }

        Name = nameValue.Value;
        Type = typeValue.Value;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks category as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        DeletedOn = deletedOn;

        MarkModify();

        return Result.Ok();
    }
}
