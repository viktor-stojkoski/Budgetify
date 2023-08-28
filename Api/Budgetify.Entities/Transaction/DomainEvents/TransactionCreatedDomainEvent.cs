namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using Budgetify.Entities.Transaction.Enumerations;

using VS.DomainEvents;

public record TransactionCreatedDomainEvent(
    int UserId,
    Guid TransactionUid,
    TransactionType TransactionType,
    decimal DifferenceAmount) : IDomainEvent;
