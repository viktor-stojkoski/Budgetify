namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using Budgetify.Entities.Transaction.Enumerations;

using VS.DomainEvents;

public record TransactionUpdatedDomainEvent(
    int UserId,
    Guid TransactionUid,
    TransactionType PreviousTransactionType,
    TransactionType TransactionType,
    int? PreviousAccountId,
    decimal? PreviousAmount,
    int? PreviousCurrencyId,
    decimal DifferenceAmount) : IDomainEvent;
