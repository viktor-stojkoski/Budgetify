namespace Budgetify.Contracts.Budget.Repositories;

using Budgetify.Entities.Budget.Domain;

public interface IBudgetRepository
{
    /// <summary>
    /// Inserts new budget.
    /// </summary>
    void Insert(Budget budget);

    /// <summary>
    /// Updates budget.
    /// </summary>
    void Update(Budget budget);
}
