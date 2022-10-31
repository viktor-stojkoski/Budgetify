namespace Budgetify.Services.Category.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Entities.Category.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record UpdateCategoryCommand(Guid CategoryUid, string? Name, string? Type) : ICommand;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        ICurrentUser currentUser,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(UpdateCategoryCommand command)
    {
        CommandResultBuilder result = new();

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result updateResult =
            categoryResult.Value.Update(
                name: command.Name,
                type: command.Type);

        if (updateResult.IsFailureOrNull)
        {
            return result.FailWith(updateResult);
        }

        _categoryRepository.Update(categoryResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
