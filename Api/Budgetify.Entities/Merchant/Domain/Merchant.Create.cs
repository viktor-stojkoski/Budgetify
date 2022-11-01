namespace Budgetify.Entities.Merchant.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Merchant.ValueObjects;

public partial class Merchant
{
    /// <summary>
    /// Create merchant DB to domain only.
    /// </summary>
    public static Result<Merchant> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        int categoryId)
    {
        Result<MerchantNameValue> nameValue = MerchantNameValue.Create(name);

        if (nameValue.IsFailureOrNull)
        {
            return Result.FromError<Merchant>(nameValue);
        }

        return Result.Ok(
            new Merchant(
                userId: userId,
                name: nameValue.Value,
                categoryId: categoryId)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates merchant.
    /// </summary>
    public static Result<Merchant> Create(
        DateTime createdOn,
        int userId,
        string? name,
        int categoryId)
    {
        Result<MerchantNameValue> nameValue = MerchantNameValue.Create(name);

        if (nameValue.IsFailureOrNull)
        {
            return Result.FromError<Merchant>(nameValue);
        }

        return Result.Ok(
            new Merchant(
                userId: userId,
                name: nameValue.Value,
                categoryId: categoryId)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
