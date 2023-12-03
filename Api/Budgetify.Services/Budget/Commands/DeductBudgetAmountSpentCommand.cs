namespace Budgetify.Services.Budget.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record DeductBudgetAmountSpentCommand(
    int UserId,
    int CurrencyId,
    int? CategoryId,
    decimal Amount,
    DateTime Date) : ICommand;

public class DeductBudgetAmountSpentCommandHandler : ICommandHandler<DeductBudgetAmountSpentCommand>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeductBudgetAmountSpentCommandHandler(
        IBudgetRepository budgetRepository,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _budgetRepository = budgetRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeductBudgetAmountSpentCommand command)
    {
        CommandResultBuilder result = new();

        Result<IEnumerable<Budget>> budgetsResult =
            await _budgetRepository.GetBudgetsByCategoryIdAsync(
                userId: command.UserId,
                categoryId: command.CategoryId);

        if (budgetsResult.IsFailureOrNull)
        {
            return result.FailWith(budgetsResult);
        }

        foreach (Budget budget in budgetsResult.Value)
        {
            decimal amount = command.Amount;

            if (budget.CurrencyId != command.CurrencyId)
            {
                Result<ExchangeRate> exchangeRateResult =
                    await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                        userId: command.UserId,
                        fromCurrencyId: command.CurrencyId,
                        toCurrencyId: budget.CurrencyId,
                        date: command.Date);

                if (exchangeRateResult.IsFailureOrNull)
                {
                    return result.FailWith(exchangeRateResult);
                }

                amount *= exchangeRateResult.Value.Rate;
            }

            Result budgetUpdateResult = budget.UpdateAmountSpent(amount);

            if (budgetUpdateResult.IsFailureOrNull)
            {
                return result.FailWith(budgetUpdateResult);
            }

            _budgetRepository.Update(budget);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
