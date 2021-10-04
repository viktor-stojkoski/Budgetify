namespace Budgetify.Services.Test
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Budgetify.Common.Commands;
    using Budgetify.Common.Storage;
    using Budgetify.Contracts.Infrastructure.Logger;
    using Budgetify.Contracts.Infrastructure.Storage;
    using Budgetify.Contracts.Settings;
    using Budgetify.Storage.Test.Entities;
    using Budgetify.Storage.Test.Repositories;

    using Microsoft.AspNetCore.Http;

    public record TestCommand(Guid TestUid, IFormFile File) : ICommand<Test>;

    public class TestCommandHandler : ICommandHandler<TestCommand, Test>
    {
        private readonly ITestRepository _testRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TestCommand> _logger;
        private readonly IStorageService _storageService;
        private readonly IStorageSettings _storageSettings;

        public TestCommandHandler(
            ITestRepository testRepository,
            IUnitOfWork unitOfWork,
            ILogger<TestCommand> logger,
            IStorageService storageService,
            IStorageSettings storageSettings)
        {
            _testRepository = testRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _storageService = storageService;
            _storageSettings = storageSettings;
        }

        public async Task<CommandResult<Test>> ExecuteAsync(TestCommand command)
        {
            CommandResultBuilder<Test> result = new();
            //command.File.

            //Result<Test> testResult = await _testRepository.GetTestAsync(command.TestUid);

            //if (testResult.IsFailure)
            //{
            //    return result.FailWith(testResult);
            //}

            //await _storageService.UploadAsync(_storageSettings.ContainerName, "test/" + command.File.FileName, await command.File.GetBytes(), command.File.ContentType);

            //Uri fileUri = await _storageService.GetSignedUrlAsync(_storageSettings.ContainerName, command.File.FileName, DateTime.UtcNow.AddDays(1));

            Stream stream = await _storageService.DownloadAsync("", command.File.FileName);

            //await _storageService.DeleteFileAsync(_storageSettings.ContainerName, command.File.FileName);

            //await _storageService.DeleteDirectoryAsync(_storageSettings.ContainerName, "test");

            //byte[] bytes = await stream.ToArrayAsync();

            //await File.WriteAllBytesAsync("File.pdf", bytes);

            _logger.LogError("TEST");
            _logger.LogException(new Exception(), "test");
            _logger.LogInformation("TEST321321321", new { test = "test321" });

#pragma warning disable CS8604 // Possible null reference argument.
            //_testRepository.Update(testResult.Value);
#pragma warning restore CS8604 // Possible null reference argument.

            //Result<Test> testResult2 = await _testRepository.GetTestAsync(Guid.Parse("fad55626-9765-4576-9ef5-e5df4aaae5a3"));

            await _unitOfWork.SaveAsync();

            result.SetValue(new Test());

            return result.Build();
        }
    }

    public static class FormFileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using MemoryStream memoryStream = new();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public static async Task<byte[]> ToArrayAsync(this Stream stream)
        {
            if (stream.Length > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("Cannot convert stream larger than max value of signed integer (2 147 483 647) to array");
            }

            byte[]? array = new byte[stream.Length];
            await stream.ReadAsync(array, 0, (int)stream.Length);
            return array;
        }
    }
}
