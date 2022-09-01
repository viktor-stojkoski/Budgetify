namespace Budgetify.Api.Registers;

using Budgetify.Api.Settings;
using Budgetify.Contracts.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static partial class Register
{
    public static IServiceCollection RegisterAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IAzureADB2CSettings azureADB2CSettings = new AzureADB2CSettings(configuration);

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(azureADB2CSettings.TenantName)
                .Build();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(azureADB2CSettings.TenantName, options =>
        {
            options.Authority = $"{azureADB2CSettings.Instance}{azureADB2CSettings.TenantId}/{azureADB2CSettings.SignUpSignInPolicyId}";
            options.Audience = azureADB2CSettings.ClientId;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = azureADB2CSettings.ClientId,
                ValidateIssuer = true,
                ValidIssuer = $"{azureADB2CSettings.Instance}{azureADB2CSettings.TenantId}/v2.0/"
            };
        });

        return services;
    }
}
