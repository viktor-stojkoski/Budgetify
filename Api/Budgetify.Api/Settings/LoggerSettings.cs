namespace Budgetify.Api.Settings
{
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
    }
}
