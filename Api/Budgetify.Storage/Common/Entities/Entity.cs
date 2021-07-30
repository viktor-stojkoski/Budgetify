namespace Budgetify.Storage.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MediatR;

    public abstract class Entity
    {
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyList<INotification> DomainEvents => _domainEvents.ToList();

        public int Id { get; protected internal set; }

        public Guid Uid { get; protected internal set; }

        public DateTime CreatedOn { get; protected internal set; }

        public DateTime? DeletedOn { get; protected internal set; }

        protected Entity() { }

        protected Entity(IEnumerable<INotification> domainEvents)
        {
            if (domainEvents is null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events cannot be null.");
            }

            _domainEvents.AddRange(domainEvents);
        }

        internal void ClearDomainEvents() => _domainEvents.Clear();
    }
}
