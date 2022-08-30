namespace Budgetify.Functions.Registers;

using System;

using Budgetify.Contracts.Infrastructure.Storage;
using Budgetify.Contracts.Settings;
using Budgetify.Functions.Settings;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.Infrastructure.UnitOfWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IConnectionStringSettings connectionStringSettings =
            new ConnectionStringSettings(configuration);

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBudgetifyDbContext, BudgetifyDbContext>();

        services.AddDbContext<BudgetifyDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString: connectionStringSettings.SqlConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(15),
                        errorNumbersToAdd: null);
                });
        });

        return services;
    }
}
