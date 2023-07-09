namespace Budgetify.Contracts.Budget.Requests;

public class UpdateBudgetRequest
{
    public string? Name { get; set; }

    public decimal Amount { get; set; }
}
