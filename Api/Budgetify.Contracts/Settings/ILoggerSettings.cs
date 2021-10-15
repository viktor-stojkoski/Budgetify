namespace Budgetify.Contracts.Settings
{
    public interface ILoggerSettings
    {
        /// <summary>
        /// Message template with data for logging.
        /// </summary>
        string DataMessageTemplate { get; }

        /// <summary>
        /// Application name.
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Application insights key.
        /// </summary>
        string ApplicationInsightsKey { get; }

        /// <summary>
        /// Starting application log template.
        /// </summary>
        string StartingAppTemplate { get; }

        /// <summary>
        /// Stopping application log template.
        /// </summary>
        string TerminatingAppTemplate { get; }
    }
}
