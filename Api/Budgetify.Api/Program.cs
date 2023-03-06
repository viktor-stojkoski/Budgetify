namespace Budgetify.Api;

using System;

using Budgetify.Api.Registers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        bool isDevelopment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        Register.InitializeLogging(configuration, isDevelopment);

        try
        {
            Register.LogStartingApp<Program>();

            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Register.LogTerminatingApp<Program>(ex);
        }
        finally
        {
            Register.CloseLogger();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .UseSerilog();
}
