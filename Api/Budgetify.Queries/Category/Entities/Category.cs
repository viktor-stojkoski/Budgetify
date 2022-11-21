namespace Budgetify.Queries.Category.Entities;

using System.Collections.Generic;

using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Merchant.Entities;
using Budgetify.Queries.Transaction.Entities;
using Budgetify.Queries.User.Entities;

public class Category : Entity
{
    public Category(
        int userId,
        string name,
        string type)
    {
        UserId = userId;
        Name = name;
        Type = type;
    }

    public int UserId { get; protected internal set; }

    public string Name { get; protected internal set; }

    public string Type { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual ICollection<Merchant> Merchants { get; protected internal set; } = new List<Merchant>();

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();
}
