namespace Budgetify.Common.CurrentUser;

public class CurrentUser : ICurrentUser
{
    public int Id { get; set; }

    public CurrentUser() { }

    public CurrentUser(int id)
    {
        Id = id;
    }
}
