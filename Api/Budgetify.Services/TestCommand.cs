namespace Budgetify.Services.Test
{
    using System;
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;
    using Budgetify.Common.Results;
    using Budgetify.Contracts.Infrastructure.Storage;
    using Budgetify.Storage.Test.Entities;
    using Budgetify.Storage.Test.Repositories;

    public record TestCommand(Guid TestUid) : ICommand<Test>;

    public class TestCommandHandler : ICommandHandler<TestCommand, Test>
    {
        private readonly ITestRepository _testRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TestCommandHandler(ITestRepository testRepository, IUnitOfWork unitOfWork)
        {
            _testRepository = testRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<Test>> ExecuteAsync(TestCommand command)
        {
            CommandResultBuilder<Test> result = new();

            Result<Test> testResult = await _testRepository.GetTestAsync(command.TestUid);

            if (testResult.IsFailure)
            {
                return result.FailWith(testResult);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            _testRepository.Update(testResult.Value);
#pragma warning restore CS8604 // Possible null reference argument.

            //Result<Test> testResult2 = await _testRepository.GetTestAsync(Guid.Parse("fad55626-9765-4576-9ef5-e5df4aaae5a3"));

            await _unitOfWork.SaveAsync();

            result.SetValue(testResult.Value);

            return result.Build();
        }
    }
}
