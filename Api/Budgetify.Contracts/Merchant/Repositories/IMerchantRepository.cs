namespace Budgetify.Contracts.Merchant.Repositories;

using Budgetify.Entities.Merchant.Domain;

public interface IMerchantRepository
{
    /// <summary>
    /// Inserts new merchant.
    /// </summary>
    void Insert(Merchant merchant);

    /// <summary>
    /// Updates merchant.
    /// </summary>
    void Update(Merchant merchant);
}
