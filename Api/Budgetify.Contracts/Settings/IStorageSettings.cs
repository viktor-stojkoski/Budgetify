namespace Budgetify.Contracts.Settings
{
    public interface IStorageSettings
    {
        /// <summary>
        /// Storage account connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Storage container name.
        /// </summary>
        string ContainerName { get; }
    }
}
