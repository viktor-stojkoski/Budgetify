namespace Budgetify.Services.User.Commands;

using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Entities.User.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record CreateUserCommand(
    string? Email,
    string? FirstName,
    string? LastName,
    string? City) : ICommand;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(CreateUserCommand command)
    {
        CommandResultBuilder result = new();

        if (await _userRepository.DoesUserEmailExistAsync(command.Email))
        {
            return result.FailWith(ResultCodes.UserAlreadyExists);
        }

        Result<User> userResult =
            User.Create(
                createdOn: System.DateTime.UtcNow,
                email: command.Email,
                firstName: command.FirstName,
                lastName: command.LastName,
                city: command.City);

        if (userResult.IsFailureOrNull)
        {
            return result.FailWith(userResult);
        }

        _userRepository.Insert(userResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
