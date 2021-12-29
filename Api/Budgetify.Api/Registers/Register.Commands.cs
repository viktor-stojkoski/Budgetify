namespace Budgetify.Api.Registers
{
    using Budgetify.Services;

    using Microsoft.Extensions.DependencyInjection;

    using VS.Commands;

    public static partial class Register
    {
        public static IServiceCollection RegisterCommands(this IServiceCollection services)
        {
            return services.AddCommands(new CommandOptions
            {
                Assemblies = new[]
                {
                    typeof(CommandsAssemblyMarker).Assembly
                }
            });
        }
    }
}
