namespace Budgetify.Entities.Transaction.DomainEvents;

using VS.DomainEvents;

public record TransactionUpdatedDomainEvent(int AccountId, decimal DifferenceAmount) : IDomainEvent;
