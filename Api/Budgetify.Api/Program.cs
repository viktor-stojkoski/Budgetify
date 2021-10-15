namespace Budgetify.Api
{
    using System;

    using Budgetify.Api.Registers;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Register.InitializeLogging(configuration);

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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
