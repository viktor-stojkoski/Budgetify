namespace Budgetify.Storage.User.Entities
{
    using Budgetify.Storage.Common.Entities;

    public class User : AggregateRoot
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; protected internal set; }

        public string Email { get; protected internal set; }
    }
}
