namespace Budgetify.Api.Registers
{
    using System;

    using Budgetify.Api.Filters;
    using Budgetify.Api.Settings;
    using Budgetify.Common.Jobs;
    using Budgetify.Contracts.Settings;

    using Hangfire;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    public static partial class Register
    {
        public static IServiceCollection RegisterHangfire(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            IJobSettings jobSettings = new JobSettings(configuration);

            services.AddHangfire(configuration =>
            {
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseSerializerSettings(new()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.All
                    })
                    .UseSqlServerStorage(jobSettings.SqlConnectionString, new()
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                    });
            });

            services.AddScoped<IJobService, HangfireJobService>();

            return services;
        }

        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            IJobSettings jobSettings = serviceScope.ServiceProvider.GetRequiredService<IJobSettings>();

            app.UseHangfireServer(new()
            {
                Queues = jobSettings.ProcessingQueues
            });

            DashboardOptions options = new()
            {
                IgnoreAntiforgeryToken = true,
                Authorization = new[]
                {
                    new HangfireAuthorizationFilter(
                        username: jobSettings.DashboardUsername,
                        password: jobSettings.DashboardPassword)
                }
            };

            app.UseHangfireDashboard(jobSettings.Endpoint, options);

            return app;
        }
    }
}
