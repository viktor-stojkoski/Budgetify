namespace Budgetify.Entities.Common.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Budgetify.Common.DomainEvents;
    using Budgetify.Entities.Common.Enumerations;

    public abstract class Entity
    {
        protected Entity() { }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// List of domain events for the entity.
        /// </summary>
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.ToList();

        /// <summary>
        /// Entity's database id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Entity's unique identifier.
        /// </summary>
        public Guid Uid { get; protected set; }

        /// <summary>
        /// Entity's date and time of creation.
        /// </summary>
        public DateTime CreatedOn { get; protected set; }

        /// <summary>
        /// Entity's date and time of deletion.
        /// </summary>
        public DateTime? DeletedOn { get; protected set; }

        /// <summary>
        /// Entity's state.
        /// </summary>
        public EntityState State { get; protected set; }

        /// <summary>
        /// Adds new domain event to the entity.
        /// </summary>
        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        /// <summary>
        /// Marks entity's state as modified.
        /// </summary>
        protected void MarkModify() => State = EntityState.Modified;

        public override int GetHashCode() => Id.GetHashCode();
    }
}
