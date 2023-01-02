namespace Budgetify.Entities.Transaction.DomainEvents;

using VS.DomainEvents;

public record TransactionCreatedDomainEvent(int AccountId, decimal DifferenceAmount) : IDomainEvent;
