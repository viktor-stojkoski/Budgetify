namespace Budgetify.Queries.Currency.Queries.GetCurrencies;

public class CurrencyResponse
{
    public CurrencyResponse(string name, string code, string? symbol)
    {
        Name = name;
        Code = code;
        Symbol = symbol;
    }

    public string Name { get; set; }

    public string Code { get; set; }

    public string? Symbol { get; set; }
}
