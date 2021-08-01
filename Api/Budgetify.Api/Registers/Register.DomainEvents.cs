namespace Budgetify.Api.Registers
{
    using Budgetify.Common.DomainEvents;
    using Budgetify.Entities;
    using Budgetify.Services;

    using Microsoft.Extensions.DependencyInjection;

    public static partial class Register
    {
        public static IServiceCollection RegisterDomainEvents(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(
                    typeof(EntitiesAssemblyMarker).Assembly,
                    typeof(CommandsAssemblyMarker).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .Decorate<IDomainEventDispatcher, ExceptionLoggingDomainEventDispatcher>();

            return services;
        }
    }
}
