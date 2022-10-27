namespace Budgetify.Common.CurrentUser;

public interface ICurrentUser
{
    /// <summary>
    /// Current logged in user's id.
    /// </summary>
    int Id { get; }
}
