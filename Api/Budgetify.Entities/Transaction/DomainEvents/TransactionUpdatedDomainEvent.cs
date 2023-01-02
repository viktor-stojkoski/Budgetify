namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using VS.DomainEvents;

public record TransactionUpdatedDomainEvent(Guid TransactionUid) : IDomainEvent;
