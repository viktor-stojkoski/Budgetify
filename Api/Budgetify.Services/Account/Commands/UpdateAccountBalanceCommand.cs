namespace Budgetify.Services.Account.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountBalanceCommand(
    int UserId,
    int AccountId,
    int CurrencyId,
    decimal Amount,
    DateTime Date,
    bool IsDeduction) : ICommand;

public class UpdateAccountBalanceCommandHandler : ICommandHandler<UpdateAccountBalanceCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountBalanceCommandHandler(
        IAccountRepository accountRepository,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountBalanceCommand command)
    {
        CommandResultBuilder result = new();

        Result<Account> accountResult =
            await _accountRepository.GetAccountByIdAsync(command.UserId, command.AccountId);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        decimal amount = command.Amount;

        if (accountResult.Value.CurrencyId != command.CurrencyId)
        {
            Result<ExchangeRate> exchangeRateResult =
                await _exchangeRateRepository.GetExchangeRateByDateAndCurrenciesAsync(
                    userId: command.UserId,
                    fromCurrencyId: command.CurrencyId,
                    toCurrencyId: accountResult.Value.CurrencyId,
                    date: command.Date);

            if (exchangeRateResult.IsFailureOrNull)
            {
                return result.FailWith(exchangeRateResult);
            }

            amount *= exchangeRateResult.Value.Rate;
        }

        Result accountUpdateResult = accountResult.Value.DeductBalance(command.IsDeduction ? amount : -amount);

        if (accountUpdateResult.IsFailureOrNull)
        {
            return result.FailWith(accountUpdateResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
