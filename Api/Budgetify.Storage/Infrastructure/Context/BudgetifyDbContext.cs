namespace Budgetify.Storage.Infrastructure.Context;

using Budgetify.Storage.Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;

public class BudgetifyDbContext : DbContext, IBudgetifyDbContext
{
    public BudgetifyDbContext(DbContextOptions<BudgetifyDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new MerchantConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new ExchangeRateConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionAttachmentConfiguration());
    }
}
