namespace Budgetify.Api.Registers
{
    using Budgetify.Common.Commands;
    using Budgetify.Services;

    using Microsoft.Extensions.DependencyInjection;

    public static partial class Register
    {
        public static IServiceCollection RegisterCommands(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(typeof(CommandAssemblyMarker).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddTransient<ICommandDispatcher, CommandDispatcher>()
                .Decorate<ICommandDispatcher, FailureLoggingCommandDispatcher>()
                .Decorate<ICommandDispatcher, ExceptionLoggingCommandDispatcher>();

            services.AddTransient<ISyncCommandDispatcher, SyncCommandDispatcher>();

            return services;
        }
    }
}
