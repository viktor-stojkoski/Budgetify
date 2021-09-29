namespace Budgetify.Contracts.Infrastructure.Logger
{
    using System;

    public interface ILogger<TCategoryName>
    {
        /// <summary>
        /// Formats and logs an error log message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void LogError(string message);

        /// <summary>
        /// Formats and logs an exception log message.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Message to log.</param>
        void LogException(Exception exception, string message);

        /// <summary>
        /// Formats and logs an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        void LogException(Exception exception);

        /// <summary>
        /// Formats and logs an information message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="data">Object data to log.</param>
        void LogInformation(string message, object data);
    }
}
