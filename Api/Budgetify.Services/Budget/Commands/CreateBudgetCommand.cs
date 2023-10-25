namespace Budgetify.Services.Budget.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.Category.Domain;
using Budgetify.Entities.Category.Enumerations;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateBudgetCommand(
    string? Name,
    Guid CategoryUid,
    string? CurrencyCode,
    DateTime StartDate,
    DateTime EndDate,
    decimal Amount,
    decimal AmountSpent) : ICommand;

public class CreateBudgetCommandHandler : ICommandHandler<CreateBudgetCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBudgetCommandHandler(
        ICurrentUser currentUser,
        ICategoryRepository categoryRepository,
        ICurrencyRepository currencyRepository,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _categoryRepository = categoryRepository;
        _currencyRepository = currencyRepository;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateBudgetCommand command)
    {
        CommandResultBuilder result = new();

        if (await _budgetRepository.DoesBudgetNameExistAsync(_currentUser.Id, command.Name))
        {
            return result.FailWith(Result.Conflicted(ResultCodes.BudgetWithSameNameAlreadyExist));
        }

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        if (categoryResult.Value.Type != CategoryType.Expense)
        {
            return result.FailWith(Result.Invalid(ResultCodes.BudgetCategoryTypeInvalid));
        }

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.CurrencyCode);

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        Result<Budget> budgetResult =
            Budget.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                name: command.Name,
                categoryId: categoryResult.Value.Id,
                currencyId: currencyResult.Value.Id,
                startDate: command.StartDate.ToLocalTime(),
                endDate: command.EndDate.ToLocalTime(),
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
