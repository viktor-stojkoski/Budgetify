namespace Budgetify.Contracts.ExchangeRate.Requests;

using System;

public class UpdateExchangeRateRequest
{
    public DateTime? FromDate { get; set; }

    public decimal Rate { get; set; }
}
