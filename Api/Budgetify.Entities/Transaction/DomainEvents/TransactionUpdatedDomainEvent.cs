namespace Budgetify.Entities.Transaction.DomainEvents;

using System;

using Budgetify.Entities.Transaction.Enumerations;

using VS.DomainEvents;

public record TransactionUpdatedDomainEvent(
    int UserId,
    Guid TransactionUid,
    TransactionType TransactionType,
    int? PreviousAccountId,
    decimal? PreviousAmount,
    int? PreviousCurrencyId,
    int? PreviousCategoryId) : IDomainEvent;
