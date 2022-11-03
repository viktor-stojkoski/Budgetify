namespace Budgetify.Storage.Merchant.Factories;

using Budgetify.Common.Results;
using Budgetify.Entities.Merchant.Domain;

internal static class MerchantFactory
{
    /// <summary>
    /// Creates <see cref="Merchant"/> domain entity for a given <see cref="Entities.Merchant"/> storage entity.
    /// </summary>
    internal static Result<Merchant> CreateMerchant(this Entities.Merchant dbMerchant)
    {
        return Merchant.Create(
            id: dbMerchant.Id,
            uid: dbMerchant.Uid,
            createdOn: dbMerchant.CreatedOn,
            deletedOn: dbMerchant.DeletedOn,
            userId: dbMerchant.UserId,
            name: dbMerchant.Name,
            categoryId: dbMerchant.CategoryId);
    }

    /// <summary>
    /// Creates <see cref="Entities.Merchant"/> storage entity for a given <see cref="Merchant"/> domain entity.
    /// </summary>
    internal static Entities.Merchant CreateMerchant(this Merchant merchant)
    {
        return new(
            id: merchant.Id,
            uid: merchant.Uid,
            createdOn: merchant.CreatedOn,
            deletedOn: merchant.DeletedOn,
            userId: merchant.UserId,
            name: merchant.Name,
            categoryId: merchant.CategoryId);
    }
}
