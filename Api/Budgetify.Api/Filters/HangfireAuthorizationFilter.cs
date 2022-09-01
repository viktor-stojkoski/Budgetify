namespace Budgetify.Api.Filters;

using System;
using System.Linq;
using System.Net;
using System.Text;

using Budgetify.Common.Extensions;

using Hangfire.Annotations;
using Hangfire.Dashboard;

using Microsoft.AspNetCore.Http;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly string _username;
    private readonly string _password;

    public HangfireAuthorizationFilter(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public bool Authorize([NotNull] DashboardContext context)
    {
        HttpContext httpContext = context.GetHttpContext();

        string authHeader = httpContext.Request.Headers["Authorization"];

        if (authHeader?.StartsWith("Basic") == true)
        {
            string? encodedUsernameAndPassword = authHeader.Split(" ").LastOrDefault();

            if (encodedUsernameAndPassword is not null)
            {
                string decodedUsernameAndPassword =
                    Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernameAndPassword));

                string? username = decodedUsernameAndPassword.Split(":").FirstOrDefault();
                string? password = decodedUsernameAndPassword.Split(":").LastOrDefault();

                return ValidateCredentials(username, password) != false
                    || Unauthorized(context, httpContext);
            }

            return true;
        }

        return Unauthorized(context, httpContext);
    }

    private bool ValidateCredentials(string? username, string? password)
        => username.HasValue() && password.HasValue()
            && _username == username && password == _password;

    private static bool Unauthorized(DashboardContext context, HttpContext httpContext)
    {
        httpContext.Response.Headers["WWW-Authenticate"] = "Basic";
        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        return false;
    }
}
