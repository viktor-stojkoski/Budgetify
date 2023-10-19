namespace Budgetify.Services.Budget.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
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
    int? PreviousCategoryId,
    decimal? PreviousAmount,
    int? PreviousCurrencyId) : ICommand;

public class UpdateBudgetAmountSpentFromTransactionAmountCommandHandler
    : ICommandHandler<UpdateBudgetAmountSpentFromTransactionAmountCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBudgetAmountSpentFromTransactionAmountCommandHandler(
        ITransactionRepository transactionRepository,
        IExchangeRateRepository exchangeRateRepository,
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
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

        if (command.PreviousCategoryId.HasValue && command.PreviousCategoryId != transactionResult.Value.CategoryId)
        {
            Result<IEnumerable<Budget>> previousBudgetsResult =
                await _budgetRepository.GetBudgetsByCategoryIdAsync(
                    userId: command.UserId,
                    categoryId: command.PreviousCategoryId.Value);

            if (previousBudgetsResult.IsFailureOrNull)
            {
                return result.FailWith(previousBudgetsResult);
            }

            foreach (Budget budget in previousBudgetsResult.Value)
            {
                decimal previousAmount = command.PreviousAmount ?? transactionResult.Value.Amount;

                if (command.PreviousCurrencyId.HasValue && budget.CurrencyId != command.PreviousCurrencyId)
                {
                    Result<ExchangeRate> previousAmountExchangeRateResult =
                        await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                            userId: command.UserId,
                            fromCurrencyId: command.PreviousCurrencyId.Value,
                            toCurrencyId: budget.CurrencyId,
                            date: transactionResult.Value.Date.Value);

                    if (previousAmountExchangeRateResult.IsFailureOrNull)
                    {
                        return result.FailWith(previousAmountExchangeRateResult);
                    }

                    previousAmount *= previousAmountExchangeRateResult.Value.Rate;
                }

                Result previousBudgetUpdateResult =
                    budget.UpdateAmountSpent(-previousAmount);

                if (previousBudgetUpdateResult.IsFailureOrNull)
                {
                    return result.FailWith(previousBudgetUpdateResult);
                }

                _budgetRepository.Update(budget);
            }
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
            decimal amount = transactionResult.Value.Amount;
            decimal previousAmount = command.PreviousAmount ?? amount;

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

            if (command.PreviousCurrencyId.HasValue && command.PreviousCurrencyId != budget.CurrencyId)
            {
                Result<ExchangeRate> exchangeRateResult =
                    await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                        userId: command.UserId,
                        fromCurrencyId: command.PreviousCurrencyId.Value,
                        toCurrencyId: budget.CurrencyId,
                        date: transactionResult.Value.Date.Value);

                if (exchangeRateResult.IsFailureOrNull)
                {
                    return result.FailWith(exchangeRateResult);
                }

                previousAmount *= exchangeRateResult.Value.Rate;
            }

            if (command.PreviousAmount.HasValue && command.PreviousCategoryId == budget.CategoryId)
            {
                amount = previousAmount > amount
                    ? -Math.Abs(previousAmount - amount)
                    : Math.Abs(previousAmount - amount);
            }

            Result previousBudgetUpdateResult =
                budget.UpdateAmountSpent(amount);

            if (previousBudgetUpdateResult.IsFailureOrNull)
            {
                return result.FailWith(previousBudgetUpdateResult);
            }

            _budgetRepository.Update(budget);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}