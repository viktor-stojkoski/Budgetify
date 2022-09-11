namespace Budgetify.Entities.Tests.User.Domain.User;

using System;

using Budgetify.Entities.User.Domain;

internal class UserBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2022, 9, 11);
    private readonly DateTime? _deletedOn = null;
    private readonly string _email = "email@email.com";
    private readonly string _firstName = "FirstName";
    private readonly string _lastName = "LastName";
    private readonly string _city = "City";

    internal User Build()
    {
        return User.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            email: _email,
            firstName: _firstName,
            lastName: _lastName,
            city: _city).Value;
    }
}
