﻿namespace Budgetify.Queries.Infrastructure.Context;

using System.Linq;

using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;

public class BudgetifyReadonlyDbContext : DbContext, IBudgetifyReadonlyDbContext
{
    public BudgetifyReadonlyDbContext(DbContextOptions<BudgetifyReadonlyDbContext> options)
        : base(options) { }

    public IQueryable<TEntity> AllNoTrackedOf<TEntity>() where TEntity : Entity
    {
        return Set<TEntity>().AsNoTracking().Where(x => x.DeletedOn == null);
    }

    public IQueryable<TEntity> SetOf<TEntity>() where TEntity : Entity
    {
        return Set<TEntity>().AsNoTracking();
    }

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
        modelBuilder.ApplyConfiguration(new BudgetConfiguration());
    }
}
