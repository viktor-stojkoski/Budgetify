namespace Budgetify.Storage.Common.Entities
{
    using System.Collections.Generic;

    using VS.DomainEvents;

    public class AggregateRoot : Entity
    {
        protected AggregateRoot() { }

        protected AggregateRoot(IEnumerable<IDomainEvent> domainEvents)
            : base(domainEvents) { }
    }
}
