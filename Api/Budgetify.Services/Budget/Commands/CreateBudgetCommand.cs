namespace Budgetify.Services.Budget.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Category.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateBudgetCommand(
    string? Name,
    Guid CategoryUid,
    DateTime StartDate,
    DateTime EndDate,
    decimal Amount,
    decimal AmountSpent) : ICommand;

public class CreateBudgetCommandHandler : ICommandHandler<CreateBudgetCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetCommandHandler(
        ICurrentUser currentUser,
        ICategoryRepository categoryRepository,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _categoryRepository = categoryRepository;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateBudgetCommand command)
    {
        CommandResultBuilder result = new();

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result<Budget> budgetResult =
            Budget.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                name: command.Name,
                categoryId: categoryResult.Value.Id,
                startDate: command.StartDate,
                endDate: command.EndDate,
                amount: command.Amount,
                amountSpent: command.AmountSpent);

        if (budgetResult.IsFailureOrNull)
        {
            return result.FailWith(budgetResult);
        }

        _budgetRepository.Insert(budgetResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
