namespace Budgetify.Queries.ExchangeRate.Queries.GetExchangeRate;

using System;

public class ExchangeRateResponse
{
    public ExchangeRateResponse(
        string fromCurrencyCode,
        string toCurrencyCode,
        DateTime? fromDate,
        DateTime? toDate,
        decimal rate)
    {
        FromCurrencyCode = fromCurrencyCode;
        ToCurrencyCode = toCurrencyCode;
        FromDate = fromDate;
        ToDate = toDate;
        Rate = rate;
    }

    public string FromCurrencyCode { get; set; }

    public string ToCurrencyCode { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal Rate { get; set; }
}
