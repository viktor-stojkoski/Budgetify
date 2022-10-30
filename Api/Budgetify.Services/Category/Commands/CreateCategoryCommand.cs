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

public record CreateCategoryCommand(string? Name, string? Type) : ICommand;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        ICurrentUser currentUser,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateCategoryCommand command)
    {
        CommandResultBuilder result = new();

        Result<Category> categoryResult =
            Category.Create(
                createdOn: DateTime.UtcNow,
                userId: _currentUser.Id,
                name: command.Name,
                type: command.Type);

        if (categoryResult.IsFailureOrNull)
        {
            return result.FailWith(categoryResult);
        }

        _categoryRepository.Insert(categoryResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
