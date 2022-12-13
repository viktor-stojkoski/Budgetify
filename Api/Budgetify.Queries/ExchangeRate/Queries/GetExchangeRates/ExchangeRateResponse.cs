namespace Budgetify.Queries.ExchangeRate.Queries.GetExchangeRates;

using System;

public class ExchangeRateResponse
{
    public ExchangeRateResponse(
        Guid uid,
        string fromCurrencyCode,
        string toCurrencyCode,
        DateTime? fromDate,
        DateTime? toDate,
        decimal rate)
    {
        Uid = uid;
        FromCurrencyCode = fromCurrencyCode;
        ToCurrencyCode = toCurrencyCode;
        FromDate = fromDate;
        ToDate = toDate;
        Rate = rate;
    }

    public Guid Uid { get; set; }

    public string FromCurrencyCode { get; set; }

    public string ToCurrencyCode { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal Rate { get; set; }
}
