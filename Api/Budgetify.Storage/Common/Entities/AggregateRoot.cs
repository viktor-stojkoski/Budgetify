namespace Budgetify.Storage.Common.Entities
{
    using System.Collections.Generic;

    using MediatR;

    public class AggregateRoot : Entity
    {
        protected AggregateRoot() { }

        protected AggregateRoot(IEnumerable<INotification> domainEvents)
            : base(domainEvents) { }
    }
}
