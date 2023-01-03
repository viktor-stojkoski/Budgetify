namespace Budgetify.Entities.ExchangeRate.DomainEvents;

using System;

using VS.DomainEvents;

public record ExchangeRateUpdatedDomainEvent(Guid ExchangeRateUid) : IDomainEvent;
