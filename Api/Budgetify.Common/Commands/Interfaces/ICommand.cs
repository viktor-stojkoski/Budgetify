namespace Budgetify.Common.Commands
{
    /// <summary>
    /// Interface that should implement every command which does return value.
    /// </summary>
    public interface ICommand<TValue> { }

    /// <summary>
    /// Interface that should implement every command which does not return value.
    /// </summary>
    public interface ICommand : ICommand<EmptyValue> { }
}
