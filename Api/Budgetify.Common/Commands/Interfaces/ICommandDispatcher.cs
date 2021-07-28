namespace Budgetify.Common.Commands
{
    using System.Threading.Tasks;

    public interface ICommandDispatcher
    {
        /// <summary>
        /// Executes the given command.
        /// </summary>
        /// <typeparam name="TValue">Return value type.</typeparam>
        /// <param name="command">Command to be executed.</param>
        /// <returns>The command return value.</returns>
        Task<CommandResult<TValue>> ExecuteAsync<TValue>(ICommand<TValue> command);
    }
}
