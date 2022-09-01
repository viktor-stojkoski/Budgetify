namespace Budgetify.Api.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class StorageSettings : IStorageSettings
{
    private readonly IConfiguration _configuration;

    public StorageSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ConnectionString => _configuration.GetValue<string>("storage:connectionString");

    public string ContainerName => _configuration.GetValue<string>("storage:containerName");
}
