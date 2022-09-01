namespace Budgetify.Api.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class LoggerSettings : ILoggerSettings
{
    private readonly IConfiguration _configuration;

    public LoggerSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string DataMessageTemplate => _configuration.GetValue<string>("logger:dataMessageTemplate");

    public string ApplicationName => _configuration.GetValue<string>("logger:applicationName");

    public string ApplicationInsightsKey => _configuration.GetValue<string>("logger:applicationInsightsKey");

    public string StartingAppTemplate => _configuration.GetValue<string>("logger:startingAppTemplate");

    public string TerminatingAppTemplate => _configuration.GetValue<string>("logger:terminatingAppTemplate");
}
