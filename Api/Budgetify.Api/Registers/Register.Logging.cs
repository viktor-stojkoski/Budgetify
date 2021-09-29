namespace Budgetify.Api.Registers
{
    using Budgetify.Contracts.Infrastructure.Logger;
    using Budgetify.Services.Infrastructure.Logger;

    using Microsoft.Extensions.DependencyInjection;

    public static partial class Register
    {
        public static IServiceCollection RegisterLogging(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            return services;
        }
    }
}
