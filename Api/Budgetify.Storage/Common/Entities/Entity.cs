namespace Budgetify.Storage.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Budgetify.Common.DomainEvents;

    public abstract class Entity
    {
        protected Entity() { }

        protected Entity(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents is null)
            {
                throw new ArgumentNullException(nameof(domainEvents), "Domain events cannot be null.");
            }

            _domainEvents.AddRange(domainEvents);
        }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// List of domain events for the entity.
        /// </summary>
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        /// <summary>
        /// Entity's database id.
        /// </summary>
        public int Id { get; protected internal set; }

        /// <summary>
        /// Entity's unique identifier.
        /// </summary>
        public Guid Uid { get; protected internal set; }

        /// <summary>
        /// Entity's date and time of creation.
        /// </summary>
        public DateTime CreatedOn { get; protected internal set; }

        /// <summary>
        /// Entity's date and time of deletion.
        /// </summary>
        public DateTime? DeletedOn { get; protected internal set; }

        /// <summary>
        /// Removes all domain events.
        /// </summary>
        internal void ClearDomainEvents() => _domainEvents.Clear();
    }
}
