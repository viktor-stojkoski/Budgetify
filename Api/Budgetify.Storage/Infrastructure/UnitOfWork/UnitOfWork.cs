namespace Budgetify.Storage.Infrastructure.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using VS.DomainEvents;

public class UnitOfWork : IUnitOfWork
{
    private readonly IBudgetifyDbContext _budgetifyDbContext;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public UnitOfWork(
        IBudgetifyDbContext budgetifyDbContext,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _budgetifyDbContext = budgetifyDbContext;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task SaveAsync()
    {
        await _budgetifyDbContext.SaveChangesAsync();

        await DispatchDomainEventsAsync();
    }

    private async Task DispatchDomainEventsAsync()
    {
        IEnumerable<EntityEntry<Entity>> domainEntities =
            _budgetifyDbContext.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any());

        IDomainEvent[] domainEvents =
            domainEntities.SelectMany(x => x.Entity.DomainEvents).ToArray();

        foreach (EntityEntry<Entity> domainEntity in domainEntities)
        {
            domainEntity.Entity.ClearDomainEvents();
        }

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _domainEventDispatcher.ExecuteAsync(domainEvent);
        }
    }

    #region Disposing

    private bool _isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _budgetifyDbContext.Dispose();
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    #endregion
}
