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

public record UpdateBudgetCommand(Guid Uid, string? Name, decimal Amount) : ICommand;

public class UpdateBudgetCommandHandler : ICommandHandler<UpdateBudgetCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBudgetCommandHandler(
        ICurrentUser currentUser,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateBudgetCommand command)
    {
        CommandResultBuilder result = new();

        Result<Budget> budgetResult =
            await _budgetRepository.GetBudgetAsync(_currentUser.Id, command.Uid);

        if (budgetResult.IsFailureOrNull)
        {
            return result.FailWith(budgetResult);
        }

        Result updateResult =
            budgetResult.Value.Update(
                name: command.Name,
                amount: command.Amount);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _budgetRepository.Update(budgetResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
