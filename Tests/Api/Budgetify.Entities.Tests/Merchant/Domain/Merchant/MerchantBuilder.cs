namespace Budgetify.Entities.Tests.Merchant.Domain.Merchant;

using System;

using Budgetify.Entities.Merchant.Domain;

internal class MerchantBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 22);
    private DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly string _name = "Name";
    private readonly int _categoryId = 3;

    internal Merchant Build()
    {
        return Merchant.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            name: _name,
            categoryId: _categoryId).Value;
    }

    internal MerchantBuilder WithDeletedOn(DateTime deletedOn)
    {
        _deletedOn = deletedOn;
        return this;
    }
}
