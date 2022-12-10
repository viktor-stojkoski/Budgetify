namespace Budgetify.Storage.Transaction.Entities;

using System;

using Budgetify.Storage.Account.Entities;
using Budgetify.Storage.Category.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Currency.Entities;
using Budgetify.Storage.Merchant.Entities;
using Budgetify.Storage.User.Entities;

public class Transaction : AggregateRoot
{
    public Transaction(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        int accountId,
        int categoryId,
        int currencyId,
        int? merchantId,
        string type,
        decimal amount,
        DateTime date,
        string? description) : base(id, uid, createdOn, deletedOn)
    {
        UserId = userId;
        AccountId = accountId;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        MerchantId = merchantId;
        Type = type;
        Amount = amount;
        Date = date;
        Description = description;
    }

    public int UserId { get; protected internal set; }

    public int AccountId { get; protected internal set; }

    public int CategoryId { get; protected internal set; }

    public int CurrencyId { get; protected internal set; }

    public int? MerchantId { get; protected internal set; }

    public string Type { get; protected internal set; }

    public decimal Amount { get; protected internal set; }

    public DateTime Date { get; protected internal set; }

    public string? Description { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Account Account { get; protected internal set; } = null!;

    public virtual Category Category { get; protected internal set; } = null!;

    public virtual Currency Currency { get; protected internal set; } = null!;

    public virtual Merchant? Merchant { get; protected internal set; } = null;
}
