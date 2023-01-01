namespace Budgetify.Services.ExchangeRate.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateExchangeRateCommand(
    string? FromCurrencyCode,
    string? ToCurrencyCode,
    DateTime? FromDate,
    decimal Rate) : ICommand;

public class CreateExchangeRateCommandHandler : ICommandHandler<CreateExchangeRateCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateExchangeRateCommandHandler(
        ICurrentUser currentUser,
        ICurrencyRepository currencyRepository,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _currencyRepository = currencyRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateExchangeRateCommand command)
    {
        CommandResultBuilder result = new();

        Result<Currency> fromCurrencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.FromCurrencyCode);

        if (fromCurrencyResult.IsFailureOrNull)
        {
            return result.FailWith(fromCurrencyResult);
        }

        Result<Currency> toCurrencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.ToCurrencyCode);

        if (toCurrencyResult.IsFailureOrNull)
        {
            return result.FailWith(toCurrencyResult);
        }

        Result<ExchangeRate> existingExchangeRateResult =
            await _exchangeRateRepository.GetExchangeRateByCurrenciesWithoutToDateAsync(
                userId: _currentUser.Id,
                fromCurrencyId: fromCurrencyResult.Value.Id,
                toCurrencyId: toCurrencyResult.Value.Id);

        if (existingExchangeRateResult.IsFailureOrNull && !existingExchangeRateResult.IsNotFound)
        {
            return result.FailWith(existingExchangeRateResult);
        }

        if (existingExchangeRateResult.IsSuccess)
        {
            if (!command.FromDate.HasValue)
            {
                return result.FailWith(Result.Invalid(ResultCodes.ExchangeRateExistsFromDateCannotBeEmpty));
            }

            Result closedResult =
                existingExchangeRateResult.Value.Close(command.FromDate.Value.ToLocalTime().AddDays(-1));

            if (closedResult.IsFailureOrNull)
            {
                return result.FailWith(closedResult);
            }

            _exchangeRateRepository.Update(existingExchangeRateResult.Value);
        }

        Result<ExchangeRate> exchangeRateResult =
            ExchangeRate.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                fromCurrencyId: fromCurrencyResult.Value.Id,
                toCurrencyId: toCurrencyResult.Value.Id,
                fromDate: command.FromDate?.ToLocalTime(),
                rate: command.Rate);

        if (exchangeRateResult.IsFailureOrNull)
        {
            return result.FailWith(exchangeRateResult);
        }

        _exchangeRateRepository.Insert(exchangeRateResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
