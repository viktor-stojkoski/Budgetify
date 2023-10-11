namespace Budgetify.Services.Transaction.DomainEventHandlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Budgetify.Common.Jobs;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Entities.Transaction.Enumerations;
using Budgetify.Services.Account.Commands;
using Budgetify.Services.Budget.Commands;

using VS.Commands;
using VS.DomainEvents;

public class TransactionDeletedDomainEventHandler : IDomainEventHandler<TransactionDeletedDomainEvent>
{
    private readonly IJobService _jobService;
    private readonly ISyncCommandDispatcher _syncCommandDispatcher;

    public TransactionDeletedDomainEventHandler(
        IJobService jobService,
        ISyncCommandDispatcher syncCommandDispatcher)
    {
        _jobService = jobService;
        _syncCommandDispatcher = syncCommandDispatcher;
    }

    public Task HandleAsync(TransactionDeletedDomainEvent @event, CancellationToken cancellationToken)
    {
        _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
            new DeductAccountBalanceCommand(
                @event.UserId,
                @event.AccountId,
                @event.CurrencyId,
                @event.Amount,
                @event.Date)));

        if (@event.TransactionType == TransactionType.Expense)
        {
            _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
                new UpdateBudgetAmountSpentFromTransactionAmountCommand(
                    @event.UserId, Guid.NewGuid(), @event.Amount)));
        }

        return Task.CompletedTask;
    }
}
