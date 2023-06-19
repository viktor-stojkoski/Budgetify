namespace Budgetify.Contracts.Budget.Requests;

using System;

public class CreateBudgetRequest
{
    public string? Name { get; set; }

    public Guid CategoryUid { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal Amount { get; set; }

    public decimal AmountSpent { get; set; }
}
