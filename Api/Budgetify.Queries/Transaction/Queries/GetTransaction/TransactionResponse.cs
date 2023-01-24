namespace Budgetify.Queries.Transaction.Queries.GetTransaction;

using System;
using System.Collections.Generic;

public class TransactionResponse
{
    public TransactionResponse(
        Guid accountUid,
        string accountName,
        Guid categoryUid,
        string categoryName,
        string currencyCode,
        Guid? merchantUid,
        string? merchantName,
        string type,
        decimal amount,
        DateTime date,
        string? description,
        IEnumerable<TransactionAttachmentResponse> transactionAttachments)
    {
        AccountUid = accountUid;
        AccountName = accountName;
        CategoryUid = categoryUid;
        CategoryName = categoryName;
        CurrencyCode = currencyCode;
        MerchantUid = merchantUid;
        MerchantName = merchantName;
        Type = type;
        Amount = amount;
        Date = date;
        Description = description;
        TransactionAttachments = transactionAttachments;
    }

    public Guid AccountUid { get; set; }

    public string AccountName { get; set; }

    public Guid CategoryUid { get; set; }

    public string CategoryName { get; set; }

    public string CurrencyCode { get; set; }

    public Guid? MerchantUid { get; set; }

    public string? MerchantName { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public IEnumerable<TransactionAttachmentResponse> TransactionAttachments { get; set; } = new List<TransactionAttachmentResponse>();
}
