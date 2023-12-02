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
            new UpdateAccountBalanceCommand(
                @event.UserId,
                @event.AccountId,
                @event.CurrencyId,
                @event.Amount,
                @event.Date,
                @event.TransactionType == TransactionType.Expense)));

        if (@event.TransactionType == TransactionType.Expense)
        {
            _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
                new DeductBudgetAmountSpentCommand(
                    @event.UserId,
                    @event.CurrencyId,
                    @event.CategoryId,
                    @event.Amount,
                    @event.Date)));
        }

        if (@event.TransactionType == TransactionType.Transfer && @event.FromAccountId.HasValue)
        {
            _jobService.Enqueue(() => _syncCommandDispatcher.Execute(
                new UpdateAccountBalanceCommand(
                    @event.UserId,
                    @event.FromAccountId.Value,
                    @event.CurrencyId,
                    @event.Amount,
                    @event.Date,
                    true)));
        }

        return Task.CompletedTask;
    }
}
