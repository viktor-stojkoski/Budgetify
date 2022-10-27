namespace Budgetify.Storage.Category.Entities;

using System;

using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.User.Entities;

public class Category : AggregateRoot
{
    public Category(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        string type) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        Name = name;
        Type = type;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Type { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;
}
