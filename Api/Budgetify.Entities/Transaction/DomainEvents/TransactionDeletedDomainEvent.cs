namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using Budgetify.Entities.Transaction.Enumerations;

using VS.DomainEvents;

public record TransactionDeletedDomainEvent(
    int UserId,
    int AccountId,
    int CurrencyId,
    int CategoryId,
    decimal Amount,
    DateTime Date,
    TransactionType TransactionType) : IDomainEvent;
