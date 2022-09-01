namespace Budgetify.Api.Settings;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class SwaggerSettings : ISwaggerSettings
{
    private readonly IConfiguration _configuration;

    public SwaggerSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string RouteTemplate => _configuration.GetValue<string>("swagger:routeTemplate");

    public string RoutePrefix => _configuration.GetValue<string>("swagger:routePrefix");

    public string Version1_0_Name => _configuration.GetValue<string>("swagger:version1_0:name");

    public string Version1_0_JsonEndpointUrl =>
        _configuration.GetValue<string>("swagger:version1_0:jsonEndpointUrl");
}
