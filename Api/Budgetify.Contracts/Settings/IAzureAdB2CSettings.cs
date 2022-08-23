namespace Budgetify.Contracts.Settings
{
    public interface IAzureADB2CSettings
    {
        /// <summary>
        /// API Connector basic authentication Username.
        /// </summary>
        string ApiConnectorUsername { get; }

        /// <summary>
        /// API Connector Basic authentication Password.
        /// </summary>
        string ApiConnectorPassword { get; }
    }
}
