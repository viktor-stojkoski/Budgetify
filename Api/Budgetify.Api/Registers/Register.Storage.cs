namespace Budgetify.Api.Registers;

using Budgetify.Common.Storage;
using Budgetify.Contracts.Settings;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterStorage(this IServiceCollection services)
    {
        services.AddSingleton<IStorageService>(provider =>
        {
            IStorageSettings storageSettings =
                provider.GetRequiredService<IStorageSettings>();

            return new AzureStorageService(storageSettings.ConnectionString);
        });

        return services;
    }
}
