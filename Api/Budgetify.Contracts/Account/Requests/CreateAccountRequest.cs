namespace Budgetify.Contracts.Account.Requests;

public class CreateAccountRequest
{
    public string? Name { get; set; }

    public string? Type { get; set; }

    public decimal Balance { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Description { get; set; }
}
