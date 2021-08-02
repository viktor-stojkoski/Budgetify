namespace Budgetify.Contracts.Settings
{
    public interface IJobSettings
    {
        /// <summary>
        /// Jobs endpoint route.
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// User name for logging in.
        /// </summary>
        string DashboardUsername { get; }

        /// <summary>
        /// Password for logging in.
        /// </summary>
        string DashboardPassword { get; }

        /// <summary>
        /// Default jobs queue.
        /// </summary>
        string DefaultQueue { get; }

        /// <summary>
        /// Supported processing queues.
        /// </summary>
        string[] ProcessingQueues { get; }

        /// <summary>
        /// Jobs connection string.
        /// </summary>
        string SqlConnectionString { get; }
    }
}
