namespace Budgetify.Services.Account.Commands;

using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Account.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateAccountBalanceFromTransactionAmountCommand(int AccountId, decimal DifferenceAmount) : ICommand;

public class UpdateAccountBalanceFromTransactionAmountCommandHandler
    : ICommandHandler<UpdateAccountBalanceFromTransactionAmountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAccountBalanceFromTransactionAmountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateAccountBalanceFromTransactionAmountCommand command)
    {
        CommandResultBuilder result = new();

        Result<Account> accountResult =
            await _accountRepository.GetAccountByIdAsync(command.AccountId);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result updateResult =
            accountResult.Value.DeductBalance(command.DifferenceAmount);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _accountRepository.Update(accountResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
