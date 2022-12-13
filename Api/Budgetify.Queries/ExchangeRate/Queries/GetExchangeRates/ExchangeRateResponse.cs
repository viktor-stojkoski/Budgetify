namespace Budgetify.Queries.ExchangeRate.Queries.GetExchangeRates;

using System;

public class ExchangeRateResponse
{
    public ExchangeRateResponse(
        Guid uid,
        string fromCurrencyCode,
        string toCurrencyCode,
        decimal rate)
    {
        Uid = uid;
        FromCurrencyCode = fromCurrencyCode;
        ToCurrencyCode = toCurrencyCode;
        Rate = rate;
    }

    public Guid Uid { get; set; }

    public string FromCurrencyCode { get; set; }

    public string ToCurrencyCode { get; set; }

    public decimal Rate { get; set; }
}
