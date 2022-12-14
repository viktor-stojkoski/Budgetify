namespace Budgetify.Contracts.ExchangeRate.Requests;

using System;

public class CreateExchangeRateRequest
{
    public string? FromCurrencyCode { get; set; }

    public string? ToCurrencyCode { get; set; }

    public DateTime? FromDate { get; set; }

    public decimal Rate { get; set; }
}
