namespace Budgetify.Services.Account.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountCommand(
    Guid AccountUid,
    string? Name,
    string? Type,
    decimal Balance,
    string? CurrencyCode,
    string? Description) : ICommand;

public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountCommandHandler(
        IAccountRepository accountRepository,
        ICurrentUser currentUser,
        ICurrencyRepository currencyRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _currentUser = currentUser;
        _currencyRepository = currencyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountCommand command)
    {
        CommandResultBuilder result = new();

        Result<Account> accountResult =
            await _accountRepository.GetAccountAsync(_currentUser.Id, command.AccountUid);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.CurrencyCode);

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        Result updateResult =
            accountResult.Value.Update(
                name: command.Name,
                type: command.Type,
                balance: command.Balance,
                currencyId: currencyResult.Value.Id,
                description: command.Description);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
