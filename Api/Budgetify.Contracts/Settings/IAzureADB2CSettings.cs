namespace Budgetify.Contracts.Settings;

using System;

public interface IAzureADB2CSettings
{
    /// <summary>
    /// App registration Client ID.
    /// </summary>
    string ClientId { get; }

    /// <summary>
    /// Tenant ID.
    /// </summary>
    string TenantId { get; }

    /// <summary>
    /// Tenant name.
    /// </summary>
    string TenantName { get; }

    /// <summary>
    /// Login instance URI.
    /// </summary>
    Uri Instance { get; }

    /// <summary>
    /// Sign Up & Sign In Policy ID.
    /// </summary>
    string SignUpSignInPolicyId { get; }
}
