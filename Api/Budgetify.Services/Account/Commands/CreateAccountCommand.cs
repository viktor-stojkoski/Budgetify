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

public record CreateAccountCommand(
    string? Name,
    string? Type,
    decimal Balance,
    string? CurrencyCode,
    string? Description) : ICommand;

public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(
        ICurrencyRepository currencyRepository,
        IAccountRepository accountRepository,
        ICurrentUser currentUser,
        IUnitOfWork unitOfWork)
    {
        _currencyRepository = currencyRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateAccountCommand command)
    {
        CommandResultBuilder result = new();

        if (await _accountRepository.DoesAccountNameExistAsync(_currentUser.Id, command.Name))
        {
            return result.FailWith(Result.Conflicted(ResultCodes.AccountWithSameNameAlreadyExist));
        }

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.CurrencyCode);

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        Result<Account> accountResult =
            Account.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                name: command.Name,
                type: command.Type,
                balance: command.Balance,
                currencyId: currencyResult.Value.Id,
                description: command.Description);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        _accountRepository.Insert(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
