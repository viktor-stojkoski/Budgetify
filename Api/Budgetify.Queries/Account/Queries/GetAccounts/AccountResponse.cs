namespace Budgetify.Queries.Account.Queries.GetAccounts;

public class AccountResponse
{
    public AccountResponse(
        string name,
        string type,
        decimal balance,
        string? description)
    {
        Name = name;
        Type = type;
        Balance = balance;
        Description = description;
    }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal Balance { get; set; }

    public string? Description { get; set; }
}
