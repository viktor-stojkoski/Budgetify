namespace Budgetify.Storage.User.Entities;

using System;

using Budgetify.Storage.Common.Entities;

public class User : AggregateRoot
{
    public User(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        string email,
        string firstName,
        string lastName,
        string city) : base(id, uid, createdOn, deletedOn)
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
