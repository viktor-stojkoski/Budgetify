namespace Budgetify.Api.Registers
{
    using Budgetify.Api.Settings;
    using Budgetify.Contracts.Settings;

    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Identity.Web;

    public static partial class Register
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options =>
                {
                    IAzureAdB2CSettings connectionStringSettings =
                        new AzureAdB2CSettings(configuration);

                    options.Instance = connectionStringSettings.Instance;
                    options.ClientId = connectionStringSettings.ClientId;
                    options.CallbackPath = connectionStringSettings.CallbackPath;
                    options.Domain = connectionStringSettings.Domain;
                    options.SignUpSignInPolicyId = connectionStringSettings.SignUpSignInPolicyId;
                    options.ResetPasswordPolicyId = connectionStringSettings.ResetPasswordPolicyId;
                    options.EditProfilePolicyId = connectionStringSettings.EditProfilePolicyId;
                });

            services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);

            return services;
        }
    }
}
