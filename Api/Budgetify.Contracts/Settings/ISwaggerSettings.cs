namespace Budgetify.Contracts.Settings;

public interface ISwaggerSettings
{
    /// <summary>
    /// Swagger's route template.
    /// </summary>
    string RouteTemplate { get; }

    /// <summary>
    /// Swagger's route prefix.
    /// </summary>
    string RoutePrefix { get; }

    /// <summary>
    /// Swagger's version 1.0 name.
    /// </summary>
    string Version1_0_Name { get; }

    /// <summary>
    /// Swagger's version 1.0 docs schema endpoint.
    /// </summary>
    string Version1_0_JsonEndpointUrl { get; }
}
