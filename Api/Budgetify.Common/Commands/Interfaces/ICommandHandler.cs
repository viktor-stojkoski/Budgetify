namespace Budgetify.Common.Commands
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler for command.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command to be executed.</typeparam>
    /// <typeparam name="TValue">Command return type.</typeparam>
    public interface ICommandHandler<in TCommand, TValue> where TCommand : ICommand<TValue>
    {
        /// <summary>
        /// Handles the given command.
        /// </summary>
        /// <param name="command">Command to be handled.</param>
        /// <returns>The command return value.</returns>
        Task<CommandResult<TValue>> ExecuteAsync(TCommand command);
    }

    /// <summary>
    /// Defines a handler for command.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command to be executed.</typeparam>
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, EmptyValue>
        where TCommand : ICommand<EmptyValue>
    { }
}
