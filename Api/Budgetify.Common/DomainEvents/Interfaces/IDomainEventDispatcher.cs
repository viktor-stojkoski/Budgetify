namespace Budgetify.Common.DomainEvents
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Asynchronously send a notification to multiple handlers.
        /// </summary>
        /// <param name="notification">Domain event</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteAsync(IDomainEvent @event, CancellationToken cancellationToken = default);
    }
}
