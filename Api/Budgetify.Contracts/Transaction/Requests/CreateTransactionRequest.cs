namespace Budgetify.Contracts.Transaction.Requests;

using System;

public class CreateTransactionRequest
{
    public Guid AccountUid { get; set; }

    public Guid CategoryUid { get; set; }

    public string? CurrencyCode { get; set; }

    public Guid? MerchantUid { get; set; }

    public string? Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }
}
