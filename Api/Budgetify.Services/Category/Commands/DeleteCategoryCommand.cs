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

public record DeleteCategoryCommand(Guid CategoryUid) : ICommand;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(
        ICurrentUser currentUser,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteCategoryCommand command)
    {
        CommandResultBuilder result = new();

        Result<Category> categoryResult =
            await _categoryRepository.GetCategoryAsync(_currentUser.Id, command.CategoryUid);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        Result deleteResult = categoryResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        _categoryRepository.Update(categoryResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
