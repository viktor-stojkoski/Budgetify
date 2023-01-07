namespace Budgetify.Services.Transaction.DomainEventHandlers;

using System.Threading;
using System.Threading.Tasks;

using Budgetify.Common.Jobs;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Services.Account.Commands;

using VS.Commands;
using VS.DomainEvents;

public class TransactionCreatedDomainEventHandler : IDomainEventHandler<TransactionCreatedDomainEvent>
{
    private readonly IJobService _jobService;
    private readonly ISyncCommandDispatcher _syncCommandDispatcher;

    public TransactionCreatedDomainEventHandler(
        IJobService jobService,
        ISyncCommandDispatcher syncCommandDispatcher)
    {
        _jobService = jobService;
        _syncCommandDispatcher = syncCommandDispatcher;
    }

    public Task HandleAsync(TransactionCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
            new UpdateAccountBalanceFromTransactionAmountCommand(
                @event.UserId, @event.TransactionUid, @event.DifferenceAmount)));

        return Task.CompletedTask;
    }
}
