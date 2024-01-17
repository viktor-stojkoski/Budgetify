namespace Budgetify.Entities.Tests.Category.Domain.Category;

using System;

using Budgetify.Entities.Category.Domain;

internal class CategoryBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 17);
    private DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly string _name = "Name";
    private readonly string _type = "EXPENSE";

    internal Category Build()
    {
        return Category.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            name: _name,
            type: _type).Value;
    }

    internal CategoryBuilder WithDeletedOn(DateTime deletedOn)
    {
        _deletedOn = deletedOn;
        return this;
    }
}
