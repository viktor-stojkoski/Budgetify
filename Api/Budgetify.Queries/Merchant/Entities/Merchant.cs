namespace Budgetify.Queries.Merchant.Entities;

using System.Collections.Generic;

using Budgetify.Queries.Category.Entities;
using Budgetify.Queries.Common.Entities;
using Budgetify.Queries.Transaction.Entities;
using Budgetify.Queries.User.Entities;

public class Merchant : Entity
{
    public Merchant(
        int userId,
        string name,
        int categoryId)
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

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();
}
