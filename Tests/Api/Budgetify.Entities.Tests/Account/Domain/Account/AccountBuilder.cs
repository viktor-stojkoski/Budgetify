namespace Budgetify.Entities.Tests.Account.Domain.Account;

using System;

using Budgetify.Entities.Account.Domain;

internal class AccountBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 13);
    private DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly string _name = "Name";
    private readonly string _type = "CASH";
    private readonly decimal _balance = 10000;
    private readonly int _currencyId = 3;
    private readonly string? _description = "Description";

    internal Account Build()
    {
        return Account.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            name: _name,
            type: _type,
            balance: _balance,
            currencyId: _currencyId,
            description: _description).Value;
    }

    internal AccountBuilder WithDeletedOn(DateTime deletedOn)
    {
        _deletedOn = deletedOn;
        return this;
    }
}
