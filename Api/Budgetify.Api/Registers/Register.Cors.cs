namespace Budgetify.Api.Registers;

using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        return services.AddCors(x => x.AddPolicy("AllowAll", builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true);

        }));
    }
}
