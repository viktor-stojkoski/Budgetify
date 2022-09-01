namespace Budgetify.Contracts.Settings;

public interface IConnectionStringSettings
{
    /// <summary>
    /// Connection string to the SQL database.
    /// </summary>
    string SqlConnectionString { get; }

    /// <summary>
    /// Read-only connection string to SQL database.
    /// </summary>
    string SqlConnectionReadonlyString { get; }
}
