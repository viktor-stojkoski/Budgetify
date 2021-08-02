namespace Budgetify.Api.Settings
{
    using Budgetify.Contracts.Settings;

    using Microsoft.Extensions.Configuration;

    public class JobSettings : IJobSettings
    {
        private readonly IConfiguration _configuration;

        public JobSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Endpoint => _configuration.GetValue<string>("jobs:endpoint");

        public string DashboardUsername => _configuration.GetValue<string>("jobs:dashboardUsername");

        public string DashboardPassword => _configuration.GetValue<string>("jobs:dashboardPassword");

        public string DefaultQueue => _configuration.GetValue<string>("jobs:defaultQueue");

        public string[] ProcessingQueues => _configuration.GetSection("jobs:processingQueues").Get<string[]>();

        public string SqlConnectionString => _configuration.GetValue<string>("jobs:sqlConnectionString");
    }
}
