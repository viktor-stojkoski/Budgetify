namespace Budgetify.Entities.Merchant.Domain;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Merchant.ValueObjects;

public sealed partial class Merchant : AggregateRoot
{
    public Merchant(
        int userId,
        MerchantNameValue name,
        int categoryId)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        Name = name;
        CategoryId = categoryId;
    }

    /// <summary>
    /// User that owns this merchant.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Merchant's name.
    /// </summary>
    public MerchantNameValue Name { get; private set; }

    /// <summary>
    /// Merchant's category.
    /// </summary>
    public int CategoryId { get; private set; }
}
