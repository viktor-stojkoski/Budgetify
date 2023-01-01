namespace Budgetify.Contracts.Merchant.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
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

    /// <summary>
    /// Returns merchant by given userId and merchantUid.
    /// </summary>
    Task<Result<Merchant>> GetMerchantAsync(int userId, Guid merchantUid);

    /// <summary>
    /// Returns boolean indicating whether merchant with the given userId and name exists.
    /// </summary>
    Task<bool> DoesMerchantNameExistAsync(int userId, string? name);
}
