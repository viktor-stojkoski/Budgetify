namespace Budgetify.Storage.Common.Entities;

using System;
using System.Collections.Generic;

using VS.DomainEvents;

public class AggregateRoot : Entity
{
    protected AggregateRoot() { }

    protected AggregateRoot(int id, Guid uid, DateTime createdOn, DateTime? deletedOn)
        : base(id, uid, createdOn, deletedOn) { }

    protected AggregateRoot(IEnumerable<IDomainEvent> domainEvents)
        : base(domainEvents) { }
}
