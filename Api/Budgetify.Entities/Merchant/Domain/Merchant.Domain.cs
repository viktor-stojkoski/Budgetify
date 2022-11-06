namespace Budgetify.Entities.Merchant.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Merchant.ValueObjects;

public partial class Merchant
{
    /// <summary>
    /// Updates merchant.
    /// </summary>
    public Result Update(string? name, int categoryId)
    {
        Result<MerchantNameValue> nameValue = MerchantNameValue.Create(name);

        if (nameValue.IsFailureOrNull)
        {
            return Result.FromError<Merchant>(nameValue);
        }

        Name = nameValue.Value;
        CategoryId = categoryId;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks merchant as deleted.
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
