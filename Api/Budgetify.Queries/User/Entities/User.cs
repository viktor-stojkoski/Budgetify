namespace Budgetify.Queries.User.Entities;

using System.Collections.Generic;

using Budgetify.Queries.Account.Entities;
using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Currency.Entities;
using Budgetify.Queries.Merchant.Entities;

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

    public virtual ICollection<Account> Accounts { get; protected internal set; } = new List<Account>();

    public virtual ICollection<Category> Categories { get; protected internal set; } = new List<Category>();

    public virtual ICollection<Merchant> Merchants { get; protected internal set; } = new List<Merchant>();
}
