namespace Budgetify.Contracts.Settings
{
    public interface ILoggerSettings
    {
        /// <summary>
        /// Message template with data for logging.
        /// </summary>
        string DataMessageTemplate { get; }
    }
}
