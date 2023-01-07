namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using VS.DomainEvents;

public record TransactionUpdatedDomainEvent(int UserId, Guid TransactionUid, decimal DifferenceAmount) : IDomainEvent;
