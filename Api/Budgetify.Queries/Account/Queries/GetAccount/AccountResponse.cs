namespace Budgetify.Queries.Account.Queries.GetAccount;

public class AccountResponse
{
    public AccountResponse(
        string name,
        string type,
        decimal balance,
        string currencyCode,
        string? description)
    {
        Name = name;
        Type = type;
        Balance = balance;
        CurrencyCode = currencyCode;
        Description = description;
    }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal Balance { get; set; }

    public string CurrencyCode { get; set; }

    public string? Description { get; set; }
}
