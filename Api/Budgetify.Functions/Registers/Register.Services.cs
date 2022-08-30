namespace Budgetify.Functions.Registers;

using Budgetify.Contracts.User.Repositories;
using Budgetify.Storage.User.Repositories;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
