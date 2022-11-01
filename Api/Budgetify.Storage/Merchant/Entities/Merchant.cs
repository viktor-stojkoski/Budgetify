namespace Budgetify.Storage.Merchant.Entities;

using System;

using Budgetify.Storage.Category.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.User.Entities;

public class Merchant : AggregateRoot
{
    public Merchant(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        int categoryId) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        Name = name;
        CategoryId = categoryId;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public int CategoryId { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Category Category { get; protected internal set; } = null!;
}
