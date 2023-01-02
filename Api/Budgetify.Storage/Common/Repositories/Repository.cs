namespace Budgetify.Storage.Common.Repositories;

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

    /// <summary>
    /// Returns query that returns entities as no tracked and not deleted.
    /// </summary>
    protected IQueryable<TEntity> AllNoTrackedOf<TEntity>() where TEntity : Entity
    {
        return _budgetifyDbContext.Set<TEntity>().Where(x => x.DeletedOn == null).AsNoTracking();
    }

    /// <summary>
    /// Returns query that returns entities which are not deleted.
    /// </summary>
    protected IQueryable<TEntity> AllOf<TEntity>() where TEntity : Entity
    {
        return _budgetifyDbContext.Set<TEntity>().Where(x => x.DeletedOn == null);
    }

    /// <summary>
    /// Inserts new aggregate root into storage.
    /// </summary>
    protected void Insert(TAggregate entity)
    {
        _budgetifyDbContext.Set<TAggregate>().Add(entity);
    }

    /// <summary>
    /// Inserts new entity into storage.
    /// </summary>
    protected void Insert<TEntity>(TEntity entity) where TEntity : Entity
    {
        _budgetifyDbContext.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Attaches or updates entity.
    /// </summary>
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
