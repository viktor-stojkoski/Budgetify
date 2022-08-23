namespace Budgetify.Functions.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class AzureADB2CSettings : IAzureADB2CSettings
{
    private readonly IConfiguration _configuration;

    public AzureADB2CSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ApiConnectorUsername =>
        _configuration.GetValue<string>("azureADB2C:apiConnectorUsername");

    public string ApiConnectorPassword =>
        _configuration.GetValue<string>("azureADB2C:apiConnectorPassword");
}
