namespace Budgetify.Common.DomainEvents
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    public class ExceptionLoggingDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IDomainEventDispatcher _inner;
        private readonly ILogger<ExceptionLoggingDomainEventDispatcher> _logger;

        public ExceptionLoggingDomainEventDispatcher(
            IDomainEventDispatcher inner,
            ILogger<ExceptionLoggingDomainEventDispatcher> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task ExecuteAsync(
            IDomainEvent @event,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _inner.ExecuteAsync(@event, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing domain event {DomainEventName} - ({@DomainEvent})",
                    @event.GetType().Name, @event);
                throw;
            }
        }
    }
}
