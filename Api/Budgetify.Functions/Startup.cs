[assembly: Microsoft.Azure.Functions.Extensions.DependencyInjection.FunctionsStartup(typeof(Budgetify.Functions.Startup))]
namespace Budgetify.Functions;

using System.IO;

using Budgetify.Functions.Registers;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        //FunctionsHostBuilderContext context = builder.GetContext();
        IConfiguration configuration = builder.GetContext().Configuration;

        builder.Services
            .RegisterServices()
            .RegisterDatabase(configuration)
            .RegisterSettings()
            .RegisterCommands();
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);

        FunctionsHostBuilderContext context = builder.GetContext();

        builder.ConfigurationBuilder
            .AddJsonFile(
                path: Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                optional: false,
                reloadOnChange: true);
    }
}
