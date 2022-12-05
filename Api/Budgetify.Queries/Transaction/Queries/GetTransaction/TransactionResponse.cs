namespace Budgetify.Queries.Transaction.Queries.GetTransaction;

using System;

public class TransactionResponse
{
    public TransactionResponse(
        string accountName,
        string categoryName,
        string currencyCode,
        string? merchantName,
        string type,
        decimal amount,
        DateTime date,
        string? description)
    {
        AccountName = accountName;
        CategoryName = categoryName;
        CurrencyCode = currencyCode;
        MerchantName = merchantName;
        Type = type;
        Amount = amount;
        Date = date;
        Description = description;
    }

    public string AccountName { get; set; }

    public string CategoryName { get; set; }

    public string CurrencyCode { get; set; }

    public string? MerchantName { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }
}
