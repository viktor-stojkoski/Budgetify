﻿namespace Budgetify.Api.Registers;

using Budgetify.Api.Settings;
using Budgetify.Contracts.Settings;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterSettings(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionStringSettings, ConnectionStringSettings>();
        services.AddSingleton<IJobSettings, JobSettings>();
        services.AddSingleton<ILoggerSettings, LoggerSettings>();
        services.AddSingleton<IStorageSettings, StorageSettings>();
        services.AddSingleton<ISwaggerSettings, SwaggerSettings>();
        services.AddSingleton<IAzureADB2CSettings, AzureADB2CSettings>();
        services.AddSingleton<IScanSettings, ScanSettings>();

        return services;
    }
}
