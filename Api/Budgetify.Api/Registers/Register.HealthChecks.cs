namespace Budgetify.Api.Registers;

using Budgetify.Api.Settings;
using Budgetify.Contracts.Settings;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static partial class Register
{
    public static IServiceCollection RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddSelfCheck()
            .AddSqlServerCheck(configuration)
            .AddHangfireCheck()
            .AddStorageCheck(configuration);

        return services;
    }

    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks(
            path: "/health",
            options: new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        app.UseHealthChecks(
            path: "/liveness",
            options: new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

        return app;
    }

    private static IHealthChecksBuilder AddSelfCheck(this IHealthChecksBuilder builder)
    {
        return builder.AddCheck("self", () => HealthCheckResult.Healthy());
    }

    private static IHealthChecksBuilder AddSqlServerCheck(this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        IConnectionStringSettings connectionStringSettings =
            new ConnectionStringSettings(configuration);

        return builder
            .AddSqlServer(
                connectionString: connectionStringSettings.SqlConnectionString,
                name: "sqlDatabase",
                tags: new string[] { "sqlDatabase" });
    }

    private static IHealthChecksBuilder AddHangfireCheck(this IHealthChecksBuilder builder)
    {
        return builder
            .AddHangfire((options) =>
            {
                options.MaximumJobsFailed = 10;
                options.MinimumAvailableServers = 1;
            },
            name: "hangfire",
            tags: new string[] { "hangfire" });
    }

    private static IHealthChecksBuilder AddStorageCheck(this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        IStorageSettings storageSettings = new StorageSettings(configuration);

        return builder
            .AddAzureBlobStorage(
                connectionString: storageSettings.ConnectionString,
                name: "azureStorage",
                tags: new string[] { "azureStorage" });
    }
}
