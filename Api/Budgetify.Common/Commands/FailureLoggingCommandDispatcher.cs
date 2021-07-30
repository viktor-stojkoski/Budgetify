namespace Budgetify.Common.Commands
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    public class FailureLoggingCommandDispatcher : ICommandDispatcher
    {
        private readonly ICommandDispatcher _inner;
        private readonly ILogger<FailureLoggingCommandDispatcher> _logger;

        public FailureLoggingCommandDispatcher(
            ICommandDispatcher inner,
            ILogger<FailureLoggingCommandDispatcher> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<CommandResult<TValue>> ExecuteAsync<TValue>(ICommand<TValue> command)
        {
            CommandResult<TValue> result = await _inner.ExecuteAsync(command);

            if (result.IsFailure)
            {
                _logger.LogWarning("Command {CommandName} failed with error {Error} - ({@Command})",
                    command.GetType().Name, result.Message, command);
            }

            return result;
        }
    }
}
