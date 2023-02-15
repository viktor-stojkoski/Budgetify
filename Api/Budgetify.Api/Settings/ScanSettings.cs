namespace Budgetify.Api.Settings;

using System;

using Budgetify.Contracts.Settings;

using Microsoft.Extensions.Configuration;

public class ScanSettings : IScanSettings
{
    private readonly IConfiguration _configuration;

    public ScanSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Uri Endpoint => _configuration.GetValue<Uri>("scan:endpoint");

    public string Key => _configuration.GetValue<string>("scan:key");

    public string ModelId => _configuration.GetValue<string>("scan:modelId");
}
