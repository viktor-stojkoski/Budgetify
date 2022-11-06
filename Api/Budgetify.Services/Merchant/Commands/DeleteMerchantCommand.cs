namespace Budgetify.Services.Merchant.Commands;

using System;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Entities.Merchant.Domain;
using Budgetify.Services.Common.Extensions;

using VS.Commands;

public record DeleteMerchantCommand(Guid MerchantUid) : ICommand;

public class DeleteMerchantCommandHandler : ICommandHandler<DeleteMerchantCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IMerchantRepository _merchantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMerchantCommandHandler(
        ICurrentUser currentUser,
        IMerchantRepository merchantRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUser = currentUser;
        _merchantRepository = merchantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommandResult<EmptyValue>> ExecuteAsync(DeleteMerchantCommand command)
    {
        CommandResultBuilder result = new();

        Result<Merchant> merchantResult =
            await _merchantRepository.GetMerchantAsync(_currentUser.Id, command.MerchantUid);

        if (merchantResult.IsFailureOrNull)
        {
            return result.FailWith(merchantResult);
        }

        Result deleteResult = merchantResult.Value.Delete(DateTime.UtcNow);

        if (deleteResult.IsFailureOrNull)
        {
            return result.FailWith(deleteResult);
        }

        _merchantRepository.Update(merchantResult.Value);

        await _unitOfWork.SaveAsync();

        return result.Build();
    }
}
