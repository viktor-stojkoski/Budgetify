namespace Budgetify.Common.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [DisplayName("Domain event: {0}")]
        public async Task ExecuteAsync(
            IDomainEvent @event,
            CancellationToken cancellationToken = default)
        {
            Type? handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(@event.GetType());

            IEnumerable<dynamic?> handlers =
                _serviceProvider.GetServices(handlerType);

            foreach (dynamic? handler in handlers)
            {
                if (handler is not null)
                {
                    await ((Task)handler.HandleAsync((dynamic)@event, cancellationToken))
                        .ConfigureAwait(false);
                }
            }
        }
    }
}
