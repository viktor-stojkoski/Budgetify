namespace Budgetify.Services.ExchangeRate.DomainEventHandlers;

using System.Threading;
using System.Threading.Tasks;

using Budgetify.Common.Jobs;
using Budgetify.Entities.ExchangeRate.DomainEvents;
using Budgetify.Services.Transaction.Commands;

using VS.Commands;
using VS.DomainEvents;

public class ExchangeRateUpdatedDomainEventHandler : IDomainEventHandler<ExchangeRateUpdatedDomainEvent>
{
    private readonly IJobService _jobService;
    private readonly ISyncCommandDispatcher _syncCommandDispatcher;

    public ExchangeRateUpdatedDomainEventHandler(
        IJobService jobService,
        ISyncCommandDispatcher syncCommandDispatcher)
    {
        _jobService = jobService;
        _syncCommandDispatcher = syncCommandDispatcher;
    }

    public Task HandleAsync(ExchangeRateUpdatedDomainEvent @event, CancellationToken cancellationToken)
    {
        _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
            new UpdateTransactionsAmountByExchangeRateCommand(@event.ExchangeRateUid)));

        return Task.CompletedTask;
    }
}
