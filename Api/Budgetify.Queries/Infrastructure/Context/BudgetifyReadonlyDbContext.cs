namespace Budgetify.Queries.Infrastructure.Context
{
    using System.Linq;

    using Budgetify.Queries.Common.Entities;

    using Microsoft.EntityFrameworkCore;

    public class BudgetifyReadonlyDbContext : DbContext, IBudgetifyReadonlyDbContext
    {
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

            // TODO: Check this
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Entity).Assembly);
        }
    }
}
