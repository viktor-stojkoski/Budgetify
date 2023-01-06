namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using VS.DomainEvents;

public record TransactionCreatedDomainEvent(int UserId, Guid TransactionUid, decimal DifferenceAmount) : IDomainEvent;
