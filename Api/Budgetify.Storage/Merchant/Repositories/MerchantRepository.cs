namespace Budgetify.Storage.Merchant.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.Merchant.Factories;

using Microsoft.EntityFrameworkCore;

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

    public async Task<Result<Merchant>> GetMerchantAsync(int userId, Guid merchantUid)
    {
        Entities.Merchant? dbMerchant = await AllNoTrackedOf<Entities.Merchant>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == merchantUid);

        if (dbMerchant is null)
        {
            return Result.NotFound<Merchant>(ResultCodes.MerchantNotFound);
        }

        return dbMerchant.CreateMerchant();
    }
}
