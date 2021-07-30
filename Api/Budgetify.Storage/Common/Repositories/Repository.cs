namespace Budgetify.Storage.Common.Repositories
{
    using System.Linq;

    using Budgetify.Storage.Common.Entities;
    using Budgetify.Storage.Infrastructure.Context;

    using Microsoft.EntityFrameworkCore;

    public class Repository<TAggregate> where TAggregate : AggregateRoot
    {
        protected readonly IBudgetifyDbContext _budgetifyDbContext;

        protected Repository(IBudgetifyDbContext budgetifyDbContext)
        {
            _budgetifyDbContext = budgetifyDbContext;
        }

        protected IQueryable<TEntity> AllNoTrackedOf<TEntity>() where TEntity : Entity
        {
            return _budgetifyDbContext.Set<TEntity>().Where(x => x.DeletedOn == null).AsNoTracking();
        }

        protected IQueryable<TEntity> AllOf<TEntity>() where TEntity : Entity
        {
            return _budgetifyDbContext.Set<TEntity>().Where(x => x.DeletedOn == null);
        }

        protected void Insert(TAggregate entity)
        {
            _budgetifyDbContext.Set<TAggregate>().Add(entity);
        }

        protected void Insert<TEntity>(TEntity entity) where TEntity : Entity
        {
            _budgetifyDbContext.Set<TEntity>().Add(entity);
        }

        protected void AttachOrUpdate<TEntity>(TEntity entity, EntityState entityState) where TEntity : Entity
        {
            TEntity? existingEntity =
                _budgetifyDbContext.Set<TEntity>().Local.SingleOrDefault(x => x.Id == entity.Id);

            if (existingEntity is null)
            {
                _budgetifyDbContext.Entry(entity).State = entityState;
            }
            else
            {
                _budgetifyDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                _budgetifyDbContext.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
