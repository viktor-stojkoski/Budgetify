namespace Budgetify.Storage.Infrastructure.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Budgetify.Contracts.Infrastructure.Storage;
    using Budgetify.Storage.Common.Entities;
    using Budgetify.Storage.Infrastructure.Context;

    using MediatR;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IBudgetifyDbContext _budgetifyDbContext;
        private readonly IMediator _mediator;

        public UnitOfWork(IBudgetifyDbContext budgetifyDbContext, IMediator mediator)
        {
            _budgetifyDbContext = budgetifyDbContext;
            _mediator = mediator;
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

            INotification[] domainEvents =
                domainEntities.SelectMany(x => x.Entity.DomainEvents).ToArray();

            foreach (EntityEntry<Entity> domainEntity in domainEntities)
            {
                domainEntity.Entity.ClearDomainEvents();
            }

            foreach (INotification domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
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
}
