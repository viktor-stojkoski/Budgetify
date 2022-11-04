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

public record UpdateMerchantCommand(Guid MerchantUid, string? Name, Guid CategoryUid) : ICommand;

public class UpdateMerchantCommandHandler : ICommandHandler<UpdateMerchantCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMerchantCommandHandler(
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

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateMerchantCommand command)
    {
        CommandResultBuilder result = new();

        Result<Merchant> merchantResult =
            await _merchantRepository.GetMerchantAsync(_currentUser.Id, command.MerchantUid);

        if (merchantResult.IsFailureOrNull)
        {
            return result.FailWith(merchantResult);
        }

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result updateResult =
            merchantResult.Value.Update(
                name: command.Name,
                categoryId: categoryResult.Value.Id);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _merchantRepository.Update(merchantResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
