namespace Budgetify.Common.Commands
{
    using System.ComponentModel;

    public class SyncCommandDispatcher : ISyncCommandDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public SyncCommandDispatcher(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [DisplayName("Command: {0}")]
        public CommandResult<TValue> Execute<TValue>(ICommand<TValue> command)
        {
            return _commandDispatcher.ExecuteAsync(command).GetAwaiter().GetResult();
        }
    }
}
