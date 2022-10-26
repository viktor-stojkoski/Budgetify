namespace Budgetify.Api.Registers;

using System.Security.Claims;

using Budgetify.Common.CurrentUser;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static partial class Register
{
    public static IServiceCollection RegisterCurrentUser(this IServiceCollection services)
    {
        return services.AddScoped(provider =>
        {
            IHttpContextAccessor? httpContextAccessor = provider.GetService<IHttpContextAccessor>();

            ClaimsIdentity? claimsIdentity =
                httpContextAccessor?.HttpContext?.User?.Identity as ClaimsIdentity;

            bool isAuthenticated = claimsIdentity?.IsAuthenticated ?? false;

            if (isAuthenticated)
            {
                return GetCurrentUserFromClaims(claimsIdentity);
            }

            return new CurrentUser();
        });
    }

    private static ICurrentUser GetCurrentUserFromClaims(ClaimsIdentity? claims)
    {
        bool intResult = int.TryParse(GetFromValue(claims, ClaimType.Id), out int id);

        if (intResult)
        {
            return new CurrentUser(id: id);
        }

        return new CurrentUser();
    }

    private static string? GetFromValue(ClaimsIdentity? claimsIdentity, string claimType) =>
        claimsIdentity?.FindFirst(claimType)?.Value ?? default;

}
