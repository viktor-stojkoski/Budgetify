namespace Budgetify.Services.Merchant.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Entities.Category.Domain;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateMerchantCommand(string? Name, Guid CategoryUid) : ICommand;

public class CreateMerchantCommandHandler : ICommandHandler<CreateMerchantCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMerchantCommandHandler(
        ICategoryRepository categoryRepository,
        ICurrentUser currentUser,
        IMerchantRepository merchantRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _merchantRepository = merchantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateMerchantCommand command)
    {
        CommandResultBuilder result = new();

        if (await _merchantRepository.DoesMerchantNameExistAsync(_currentUser.Id, command.Name))
        {
            return result.FailWith(Result.Conflicted(ResultCodes.MerchantWithSameNameAlreadyExist));
        }

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result<Merchant> merchantResult =
            Merchant.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                name: command.Name,
                categoryId: categoryResult.Value.Id);

        if (merchantResult.IsFailureOrNull)
        {
            return result.FailWith(merchantResult);
        }

        _merchantRepository.Insert(merchantResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
