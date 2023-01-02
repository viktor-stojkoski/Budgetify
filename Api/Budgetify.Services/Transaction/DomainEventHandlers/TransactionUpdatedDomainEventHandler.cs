namespace Budgetify.Services.Transaction.DomainEventHandlers;

using System.Threading;
using System.Threading.Tasks;

using Budgetify.Common.Jobs;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Services.Account.Commands;

using VS.Commands;
using VS.DomainEvents;

public class TransactionUpdatedDomainEventHandler : IDomainEventHandler<TransactionUpdatedDomainEvent>
{
    private readonly IJobService _jobService;
    private readonly ISyncCommandDispatcher _syncCommandDispatcher;

    public TransactionUpdatedDomainEventHandler(
        IJobService jobService,
        ISyncCommandDispatcher syncCommandDispatcher)
    {
        _jobService = jobService;
        _syncCommandDispatcher = syncCommandDispatcher;
    }

    public Task HandleAsync(TransactionUpdatedDomainEvent @event, CancellationToken cancellationToken)
    {
        _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
            new UpdateAccountBalanceFromTransactionAmountCommand(
                @event.AccountId, @event.DifferenceAmount)));

        return Task.CompletedTask;
    }
}
