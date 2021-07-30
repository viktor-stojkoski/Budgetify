namespace Budgetify.Common.Commands
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    public class ExceptionLoggingCommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandDispatcher _inner;
        private readonly ILogger<ExceptionLoggingCommandDispatcher> _logger;

        public ExceptionLoggingCommandDispatcher(
            ICommandDispatcher inner,
            ILogger<ExceptionLoggingCommandDispatcher> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<CommandResult<TValue>> ExecuteAsync<TValue>(ICommand<TValue> command)
        {
            try
            {
                return await _inner.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing command {CommandName} - ({@Command})",
                    command.GetType().Name, command);
                throw;
            }
        }
    }
}
