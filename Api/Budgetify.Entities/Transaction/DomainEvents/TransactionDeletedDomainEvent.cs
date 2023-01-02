namespace Budgetify.Entities.Transaction.DomainEvents;

using VS.DomainEvents;

public record TransactionDeletedDomainEvent(int AccountId, decimal DifferenceAmount) : IDomainEvent;
