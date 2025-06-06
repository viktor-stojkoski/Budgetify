﻿namespace Budgetify.Entities.Transaction.Domain;

using System;
using System.Collections.Generic;

using Budgetify.Entities.Common.Entities;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.Enumerations;

public sealed partial class Transaction : AggregateRoot
{
    private Transaction(
        int userId,
        int? accountId,
        int? fromAccountId,
        int? categoryId,
        int currencyId,
        int? merchantId,
        TransactionType type,
        decimal amount,
        DateTime? date,
        string? description,
        bool isVerified)
    {
        State = EntityState.Unchanged;

        UserId = userId;
        AccountId = accountId;
        FromAccountId = fromAccountId;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        MerchantId = merchantId;
        Type = type;
        Amount = amount;
        Date = date;
        Description = description;
        IsVerified = isVerified;
    }

    private readonly List<TransactionAttachment> _attachments = new();

    /// <summary>
    /// Attachments for the transaction.
    /// </summary>
    public IReadOnlyList<TransactionAttachment> Attachments => _attachments;

    /// <summary>
    /// User that owns this transaction.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Transaction's account.
    /// </summary>
    public int? AccountId { get; private set; }

    /// <summary>
    /// Transaction's from account (Used only in transfer).
    /// </summary>
    public int? FromAccountId { get; private set; }

    /// <summary>
    /// Transaction's category.
    /// </summary>
    public int? CategoryId { get; private set; }

    /// <summary>
    /// Transaction's currency.
    /// </summary>
    public int CurrencyId { get; private set; }

    /// <summary>
    /// Transaction's merchant.
    /// </summary>
    public int? MerchantId { get; private set; }

    /// <summary>
    /// Transaction's type.
    /// </summary>
    public TransactionType Type { get; private set; }

    /// <summary>
    /// Transaction's amount.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// Transaction's date.
    /// </summary>
    public DateTime? Date { get; private set; }

    /// <summary>
    /// Transaction's description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Boolean indicating whether the transaction is verified or not.
    /// </summary>
    public bool IsVerified { get; private set; }
}
