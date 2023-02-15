namespace Budgetify.Api.Registers;

using Budgetify.Common.ScanReceipt;
using Budgetify.Contracts.Account.Repositories;
using Budgetify.Contracts.Category.Repositories;
using Budgetify.Contracts.Currency.Repositories;
using Budgetify.Contracts.ExchangeRate.Repositories;
using Budgetify.Contracts.Merchant.Repositories;
using Budgetify.Contracts.Settings;
using Budgetify.Contracts.Transaction.Repositories;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Storage.Account.Repositories;
using Budgetify.Storage.Category.Repositories;
using Budgetify.Storage.Currency.Repositories;
using Budgetify.Storage.ExchangeRate.Repositories;
using Budgetify.Storage.Merchant.Repositories;
using Budgetify.Storage.Transaction.Repositories;
using Budgetify.Storage.User.Repositories;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMerchantRepository, MerchantRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        services.AddScoped<IScanReceiptService>(provider =>
        {
            IScanSettings scanSettings = provider.GetRequiredService<IScanSettings>();

            return new ScanReceiptService(
                endpoint: scanSettings.Endpoint,
                key: scanSettings.Key,
                modelId: scanSettings.ModelId);
        });

        return services;
    }
}
