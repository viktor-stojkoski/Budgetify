namespace Budgetify.Common.DomainEvents
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler for a domain event.
    /// </summary>
    /// <typeparam name="TDomainEvent">Type of the domain event to be handled.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Handles domain event.
        /// </summary>
        /// <param name="event">The event to be handled.</param>
        /// <returns></returns>
        Task HandleAsync(TDomainEvent @event, CancellationToken cancellationToken);
    }
}
