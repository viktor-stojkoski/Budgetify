namespace Budgetify.Storage.Infrastructure.Context
{
    using Budgetify.Storage.Infrastructure.Configuration;

    using Microsoft.EntityFrameworkCore;

    public class BudgetifyDbContext : DbContext, IBudgetifyDbContext
    {
        public BudgetifyDbContext(DbContextOptions<BudgetifyDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TestConfiguration());
        }
    }
}
