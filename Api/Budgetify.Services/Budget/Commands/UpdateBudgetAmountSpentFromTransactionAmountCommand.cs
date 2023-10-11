namespace Budgetify.Services.Budget.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateBudgetAmountSpentFromTransactionAmountCommand(
    int UserId,
    Guid TransactionUid,
    decimal DifferenceAmount) : ICommand;

public class UpdateBudgetAmountSpentFromTransactionAmountCommandHandler
    : ICommandHandler<UpdateBudgetAmountSpentFromTransactionAmountCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBudgetAmountSpentFromTransactionAmountCommandHandler(
        ITransactionRepository transactionRepository,
        IExchangeRateRepository exchangeRateRepository,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork,
        ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateBudgetAmountSpentFromTransactionAmountCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionAsync(command.UserId, command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        if (!transactionResult.Value.CategoryId.HasValue || !transactionResult.Value.Date.HasValue)
        {
            return result.FailWith(Result.Invalid(ResultCodes.TransactionNotVerifiedCannotUpdateBudgetAmountSpent));
        }

        Result<IEnumerable<Budget>> budgetsResult =
            await _budgetRepository.GetBudgetsByCategoryIdAsync(
                userId: command.UserId,
                categoryId: transactionResult.Value.CategoryId.Value);

        if (budgetsResult.IsFailureOrNull)
        {
            return result.FailWith(budgetsResult);
        }

        foreach (Budget budget in budgetsResult.Value)
        {
            decimal amount = command.DifferenceAmount;

            if (transactionResult.Value.CurrencyId != budget.CurrencyId)
            {
                Result<ExchangeRate> exchangeRateResult =
                    await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                        userId: command.UserId,
                        fromCurrencyId: transactionResult.Value.CurrencyId,
                        toCurrencyId: budget.CurrencyId,
                        date: transactionResult.Value.Date.Value);

                if (exchangeRateResult.IsFailureOrNull)
                {
                    return result.FailWith(exchangeRateResult);
                }

                amount *= exchangeRateResult.Value.Rate;
            }

            Result updateResult = budget.UpdateAmountSpent(amount);

            if (updateResult.IsFailureOrNull)
            {
                return result.FailWith(updateResult);
            }

            _budgetRepository.Update(budget);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}