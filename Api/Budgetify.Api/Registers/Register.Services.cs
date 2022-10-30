namespace Budgetify.Api.Registers;

using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Storage.Account.Repositories;
using Budgetify.Storage.Category.Repositories;
using Budgetify.Storage.Currency.Repositories;
using Budgetify.Storage.Test.Repositories;
using Budgetify.Storage.User.Repositories;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
