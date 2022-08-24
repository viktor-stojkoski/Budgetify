namespace Budgetify.Functions.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class AzureADB2CApiConnectorSettings : IAzureADB2CApiConnectorSettings
{
    private readonly IConfiguration _configuration;

    public AzureADB2CApiConnectorSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Username =>
        _configuration.GetValue<string>("azureADB2CApiConnector:username");

    public string Password =>
        _configuration.GetValue<string>("azureADB2CApiConnector:password");
}
