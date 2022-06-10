namespace Budgetify.Queries.User.Entities
{
    using Budgetify.Queries.Common.Entities;

    public class User : Entity
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
