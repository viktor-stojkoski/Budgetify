namespace Budgetify.Api.Registers
{
    using Budgetify.Entities;
    using Budgetify.Services;

    using Microsoft.Extensions.DependencyInjection;

    using VS.DomainEvents;

    public static partial class Register
    {
        public static IServiceCollection RegisterDomainEvents(this IServiceCollection services)
        {
            return services.AddDomainEvents(new DomainEventsOptions
            {
                Assemblies = new[]
                {
                    typeof(EntitiesAssemblyMarker).Assembly,
                    typeof(CommandsAssemblyMarker).Assembly
                }
            });
        }
    }
}
