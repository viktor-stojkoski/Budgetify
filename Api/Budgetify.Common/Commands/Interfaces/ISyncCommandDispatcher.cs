namespace Budgetify.Common.Commands
{
    public interface ISyncCommandDispatcher
    {
        /// <summary>
        /// Handles the given command.
        /// </summary>
        /// <param name="command">Command to be handled.</param>
        /// <returns>The command return value.</returns>
        CommandResult<TValue> Execute<TValue>(ICommand<TValue> command);
    }
}
