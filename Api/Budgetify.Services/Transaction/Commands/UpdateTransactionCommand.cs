namespace Budgetify.Services.Transaction.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Entities.Account.Domain;
using Budgetify.Entities.Category.Domain;
using Budgetify.Entities.Currency.Domain;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Entities.Transaction.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateTransactionCommand(
    Guid TransactionUid,
    Guid AccountUid,
    Guid CategoryUid,
    string? CurrencyCode,
    Guid? MerchantUid,
    decimal Amount,
    DateTime Date,
    string? Description) : ICommand;

public class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAccountRepository _accountRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMerchantRepository _merchantRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransactionCommandHandler(
        ICurrentUser currentUser,
        IAccountRepository accountRepository,
        ICategoryRepository categoryRepository,
        ICurrencyRepository currencyRepository,
        IMerchantRepository merchantRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
        _currencyRepository = currencyRepository;
        _merchantRepository = merchantRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateTransactionCommand command)
    {
        CommandResultBuilder result = new();

        Result<Transaction> transactionResult =
            await _transactionRepository.GetTransactionAsync(_currentUser.Id, command.TransactionUid);

        if (transactionResult.IsFailureOrNull)
        {
            return result.FailWith(transactionResult);
        }

        Result<Account> accountResult =
            await _accountRepository.GetAccountAsync(_currentUser.Id, command.AccountUid);

        if (accountResult.IsFailureOrNull)
        {
            return result.FailWith(accountResult);
        }

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result<Currency> currencyResult =
            await _currencyRepository.GetCurrencyByCodeAsync(command.CurrencyCode);

        if (currencyResult.IsFailureOrNull)
        {
            return result.FailWith(currencyResult);
        }

        int? merchantId = null;

        if (command.MerchantUid.HasValue)
        {
            Result<Merchant> merchantResult =
                await _merchantRepository.GetMerchantAsync(_currentUser.Id, command.MerchantUid.Value);

            if (merchantResult.IsFailureOrNull)
            {
                return result.FailWith(merchantResult);
            }

            merchantId = merchantResult.Value.Id;
        }

        Result updateResult =
            transactionResult.Value.Update(
                accountId: accountResult.Value.Id,
                categoryId: categoryResult.Value.Id,
                currencyId: currencyResult.Value.Id,
                merchantId: merchantId,
                amount: command.Amount,
                date: command.Date.ToLocalTime(),
                description: command.Description);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _transactionRepository.Update(transactionResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
