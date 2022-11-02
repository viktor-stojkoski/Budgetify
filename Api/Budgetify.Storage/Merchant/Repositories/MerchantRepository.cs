namespace Budgetify.Storage.Merchant.Repositories;

using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.Merchant.Factories;

public class MerchantRepository : Repository<Entities.Merchant>, IMerchantRepository
{
    public MerchantRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Merchant merchant)
    {
        Entities.Merchant dbMerchant = merchant.CreateMerchant();

        Insert(dbMerchant);
    }

    public void Update(Merchant merchant)
    {
        Entities.Merchant dbMerchant = merchant.CreateMerchant();

        AttachOrUpdate(dbMerchant, merchant.State.GetState());
    }
}
