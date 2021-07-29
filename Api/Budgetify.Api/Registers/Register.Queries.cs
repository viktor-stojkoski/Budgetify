namespace Budgetify.Api.Registers
{
    using Budgetify.Common.Queries;
    using Budgetify.Queries;

    using Microsoft.Extensions.DependencyInjection;

    public static partial class Register
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(typeof(QueriesAssemblyMarker).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.AddTransient<IQueryDispatcher, QueryDispatcher>()
                .Decorate<IQueryDispatcher, ExceptionLoggingQueryDispatcher>();

            return services;
        }
    }
}
