namespace Budgetify.Contracts.Settings;

using System;

public interface IScanSettings
{
    /// <summary>
    /// Scan endpoint.
    /// </summary>
    Uri Endpoint { get; }

    /// <summary>
    /// Scan key.
    /// </summary>
    string Key { get; }
}
