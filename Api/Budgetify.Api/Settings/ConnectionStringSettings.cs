namespace Budgetify.Api.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class ConnectionStringSettings : IConnectionStringSettings
{
    private readonly IConfiguration _configuration;

    public ConnectionStringSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string SqlConnectionString =>
        _configuration.GetValue<string>("connectionStrings:sqlConnectionString");

    public string SqlConnectionReadonlyString =>
        _configuration.GetValue<string>("connectionStrings:sqlConnectionReadonlyString");
}
