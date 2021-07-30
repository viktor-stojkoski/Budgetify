namespace Budgetify.Common.Commands
{
    /// <summary>
    /// Marker interface to represent command which does return value.
    /// </summary>
    public interface ICommand<TValue> { }

    /// <summary>
    /// Marker interface to represent command which does not return value.
    /// </summary>
    public interface ICommand : ICommand<EmptyValue> { }
}
