namespace Budgetify.Common.Commands
{
    using System.Threading.Tasks;

    public interface ICommandHandler<in TCommand, TValue> where TCommand : ICommand<TValue>
    {
        /// <summary>
        /// Handles the given command.
        /// </summary>
        /// <param name="command">Command to be handled.</param>
        /// <returns>The command return value.</returns>
        Task<CommandResult<TValue>> ExecuteAsync(TCommand command);
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, EmptyValue>
        where TCommand : ICommand<EmptyValue>
    { }
}
