namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using VS.DomainEvents;

public record TransactionCreatedDomainEvent(Guid TransactionUid) : IDomainEvent;
