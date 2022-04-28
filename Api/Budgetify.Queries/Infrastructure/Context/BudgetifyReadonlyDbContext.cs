namespace Budgetify.Queries.Infrastructure.Context
{
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

            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
