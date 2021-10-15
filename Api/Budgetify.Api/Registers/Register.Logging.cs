namespace Budgetify.Api.Registers
{
    using System;
    using System.IO;

    using Budgetify.Api.Settings;
    using Budgetify.Contracts.Infrastructure.Logger;
    using Budgetify.Contracts.Settings;
    using Budgetify.Services.Infrastructure.Logger;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Serilog;
    using Serilog.Exceptions;

    public static partial class Register
    {
        private static ILoggerSettings? LoggerSettings { get; set; }

        public static IServiceCollection RegisterLoggingServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            return services;
        }

        public static IServiceCollection RegisterLogging(this IServiceCollection services)
        {
            return services.AddApplicationInsightsTelemetry();
        }

        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder builder)
        {
            return builder.UseSerilog();
        }

        public static void InitializeLogging(IConfiguration configuration)
        {
            LoggerSettings = new LoggerSettings(configuration);

            Serilog.Core.Logger logger =
                new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty(nameof(LoggerSettings.ApplicationName), LoggerSettings.ApplicationName)
                .WriteTo.Console()
                .WriteTo.File(
                    path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 3,
                    fileSizeLimitBytes: 1000000,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.ApplicationInsights(
                    instrumentationKey: LoggerSettings.ApplicationInsightsKey,
                    telemetryConverter: TelemetryConverter.Traces)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Logger = logger;
        }

        public static void LogStartingApp<TStartupLocation>() =>
            Log.Logger.ForContext<TStartupLocation>()
                .Information(LoggerSettings?.StartingAppTemplate, LoggerSettings?.ApplicationName);

        public static void LogTerminatingApp<TStartupLocation>(Exception ex) =>
            Log.Logger.ForContext<TStartupLocation>()
                .Fatal(ex, LoggerSettings?.TerminatingAppTemplate, LoggerSettings?.ApplicationName);

        public static void CloseLogger() => Log.CloseAndFlush();
    }
}
