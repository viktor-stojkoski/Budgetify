namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateTransactionsAmountByExchangeRateCommand(Guid ExchangeRateUid) : ICommand;

public class UpdateTransactionsAmountByExchangeRateCommandHandler
    : ICommandHandler<UpdateTransactionsAmountByExchangeRateCommand>
{
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransactionsAmountByExchangeRateCommandHandler(
        IExchangeRateRepository exchangeRateRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _exchangeRateRepository = exchangeRateRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateTransactionsAmountByExchangeRateCommand command)
    {
        CommandResultBuilder result = new();

        Result<ExchangeRate> exchangeRateResult =
            await _exchangeRateRepository.GetExchangeRateByUidAsync(command.ExchangeRateUid);

        if (exchangeRateResult.IsFailureOrNull)
        {
            return result.FailWith(exchangeRateResult);
        }

        Result<IEnumerable<Transaction>> transactionsResult =
            await _transactionRepository.GetTransactionsInDateRangeAsync(
                fromDate: exchangeRateResult.Value.DateRange.FromDate,
                toDate: exchangeRateResult.Value.DateRange.ToDate);

        if (transactionsResult.IsFailureOrNull)
        {
            return result.FailWith(transactionsResult);
        }

        foreach (Transaction transaction in transactionsResult.Value)
        {
            Result updateResult =
                transaction.UpdateAmount(transaction.Amount * exchangeRateResult.Value.Rate);

            if (updateResult.IsFailureOrNull)
            {
                return result.FailWith(updateResult);
            }

            _transactionRepository.Update(transaction);
        }

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
