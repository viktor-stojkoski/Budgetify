namespace Budgetify.Entities.ExchangeRate.DomainEvents;

using System;

using VS.DomainEvents;

public record ExchangeRateUpdatedDomainEvent(int UserId, Guid ExchangeRateUid, decimal PreviousRate) : IDomainEvent;
