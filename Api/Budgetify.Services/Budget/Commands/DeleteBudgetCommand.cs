namespace Budgetify.Services.Budget.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record DeleteBudgetCommand(Guid BudgetUid) : ICommand;

public class DeleteBudgetCommandHandler : ICommandHandler<DeleteBudgetCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBudgetCommandHandler(
        ICurrentUser currentUser,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteBudgetCommand command)
    {
        CommandResultBuilder result = new();

        Result<Budget> budgetResult =
            await _budgetRepository.GetBudgetAsync(_currentUser.Id, command.BudgetUid);

        if (budgetResult.IsFailureOrNull)
        {
            return result.FailWith(budgetResult);
        }

        Result deleteResult = budgetResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        _budgetRepository.Update(budgetResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
