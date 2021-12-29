namespace Budgetify.Api.Registers
{
    using Budgetify.Queries;

    using Microsoft.Extensions.DependencyInjection;

    using VS.Queries;

    public static partial class Register
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            return services.AddQueries(new QueriesOptions
            {
                Assemblies = new[]
                {
                    typeof(QueriesAssemblyMarker).Assembly
                }
            });
        }
    }
}
