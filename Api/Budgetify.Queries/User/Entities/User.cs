namespace Budgetify.Queries.User.Entities
{
    using Budgetify.Queries.Common.Entities;

    public class User : Entity
    {
        public User(
            string email,
            string firstName,
            string lastName,
            string city)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }

        public string Email { get; protected internal set; }

        public string FirstName { get; protected internal set; }

        public string LastName { get; protected internal set; }

        public string City { get; protected internal set; }
    }
}
