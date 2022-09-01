namespace Budgetify.Api.Registers;

using Budgetify.Contracts.User.Repositories;
using Budgetify.Storage.Test.Repositories;
using Budgetify.Storage.User.Repositories;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
