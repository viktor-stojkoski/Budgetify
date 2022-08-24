namespace Budgetify.Functions.Registers;

using Budgetify.Contracts.Settings;
using Budgetify.Functions.Settings;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterSettings(this IServiceCollection services)
    {
        services.AddSingleton<IAzureADB2CApiConnectorSettings, AzureADB2CApiConnectorSettings>();

        return services;
    }
}
