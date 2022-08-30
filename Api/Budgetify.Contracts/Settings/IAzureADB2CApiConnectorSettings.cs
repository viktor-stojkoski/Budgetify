namespace Budgetify.Contracts.Settings
{
    public interface IAzureADB2CApiConnectorSettings
    {
        /// <summary>
        /// API Connector basic authentication Username.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// API Connector Basic authentication Password.
        /// </summary>
        string Password { get; }
    }
}
