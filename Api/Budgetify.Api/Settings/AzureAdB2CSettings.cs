namespace Budgetify.Api.Settings
{
    using Budgetify.Contracts.Settings;

    using Microsoft.Extensions.Configuration;

    public class AzureAdB2CSettings : IAzureAdB2CSettings
    {
        private readonly IConfiguration _configuration;

        public AzureAdB2CSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Instance => _configuration.GetValue<string>("azureAdB2C:instance");

        public string ClientId => _configuration.GetValue<string>("azureAdB2C:clientId");

        public string CallbackPath => _configuration.GetValue<string>("azureAdB2C:callbackPath");

        public string Domain => _configuration.GetValue<string>("azureAdB2C:domain");

        public string SignUpSignInPolicyId => _configuration.GetValue<string>("azureAdB2C:signUpSignInPolicyId");

        public string ResetPasswordPolicyId => _configuration.GetValue<string>("azureAdB2C:resetPasswordPolicyId");

        public string EditProfilePolicyId => _configuration.GetValue<string>("azureAdB2C:editProfilePolicyId");
    }
}
