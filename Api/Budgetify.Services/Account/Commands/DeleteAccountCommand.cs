namespace Budgetify.Services.Account.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record DeleteAccountCommand(Guid AccountUid) : ICommand;

public class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountCommandHandler(
        IUserRepository userRepository,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteAccountCommand command)
    {
        CommandResultBuilder result = new();

        Result<Account> accountResult =
            await _accountRepository.GetAccountAsync(command.AccountUid);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result deleteResult = accountResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
