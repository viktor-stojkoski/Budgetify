namespace Budgetify.Services.ExchangeRate.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateExchangeRateCommand(Guid ExchangeRateUid, DateTime? FromDate, decimal Rate) : ICommand;

public class UpdateExchangeRateCommandHandler : ICommandHandler<UpdateExchangeRateCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExchangeRateCommandHandler(
        ICurrentUser currentUser,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateExchangeRateCommand command)
    {
        CommandResultBuilder result = new();

        Result<ExchangeRate> exchangeRateResult =
            await _exchangeRateRepository.GetExchangeRateAsync(_currentUser.Id, command.ExchangeRateUid);

        if (exchangeRateResult.IsFailureOrNull)
        {
            return result.FailWith(exchangeRateResult);
        }

        Result<ExchangeRate> lastClosedExchangeRateResult =
            await _exchangeRateRepository.GetLastClosedExchangeRateByCurrencies(
                userId: _currentUser.Id,
                fromCurrencyId: exchangeRateResult.Value.FromCurrencyId,
                toCurrencyId: exchangeRateResult.Value.ToCurrencyId);

        if (lastClosedExchangeRateResult.IsFailureOrNull && !lastClosedExchangeRateResult.IsNotFound)
        {
            return result.FailWith(lastClosedExchangeRateResult);
        }

        if (lastClosedExchangeRateResult.IsSuccess)
        {
            if (!command.FromDate.HasValue)
            {
                return result.FailWith(Result.Invalid(ResultCodes.ExchangeRateExistsFromDateCannotBeEmpty));
            }

            Result closedResult =
                lastClosedExchangeRateResult.Value.Close(command.FromDate.Value.ToLocalTime().AddDays(-1));

            if (closedResult.IsFailureOrNull)
            {
                return result.FailWith(closedResult);
            }

            _exchangeRateRepository.Update(lastClosedExchangeRateResult.Value);
        }

        Result updateResult =
            exchangeRateResult.Value.Update(command.FromDate, command.Rate);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _exchangeRateRepository.Update(exchangeRateResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
