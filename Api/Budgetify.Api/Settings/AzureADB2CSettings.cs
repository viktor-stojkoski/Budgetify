namespace Budgetify.Api.Settings
{
    using System;

    using Budgetify.Contracts.Settings;

    using Microsoft.Extensions.Configuration;

    public class AzureADB2CSettings : IAzureADB2CSettings
    {
        private readonly IConfiguration _configuration;

        public AzureADB2CSettings(IConfiguration configuration) =>
            _configuration = configuration;

        public string ClientId => _configuration.GetValue<string>("azureADB2C:clientId");

        public string TenantId => _configuration.GetValue<string>("azureADB2C:tenantId");

        public string TenantName => _configuration.GetValue<string>("azureADB2C:tenantName");

        public Uri Instance => _configuration.GetValue<Uri>("azureADB2C:instance");

        public string SignUpSignInPolicyId =>
            _configuration.GetValue<string>("azureADB2C:signUpSignInPolicyId");
    }
}
