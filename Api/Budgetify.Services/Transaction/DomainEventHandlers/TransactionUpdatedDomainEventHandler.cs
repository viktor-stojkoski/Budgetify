namespace Budgetify.Services.Transaction.DomainEventHandlers;

using System.Threading;
using System.Threading.Tasks;

using Budgetify.Common.Jobs;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Entities.Transaction.Enumerations;
using Budgetify.Services.Account.Commands;
using Budgetify.Services.Budget.Commands;

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
            new UpdateAccountsBalanceFromTransactionCommand(
                @event.UserId,
                @event.TransactionUid,
                @event.PreviousAccountId,
                @event.PreviousFromAccountId,
                @event.PreviousAmount,
                @event.PreviousCurrencyId)));

        if (@event.TransactionType == TransactionType.Expense)
        {
            _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
                new UpdateBudgetAmountSpentFromTransactionAmountCommand(
                    @event.UserId,
                    @event.TransactionUid,
                    @event.PreviousCategoryId,
                    @event.PreviousAmount,
                    @event.PreviousCurrencyId)));
        }

        return Task.CompletedTask;
    }
}
