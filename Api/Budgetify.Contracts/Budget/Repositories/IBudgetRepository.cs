namespace Budgetify.Contracts.Budget.Repositories;

using System.Threading.Tasks;

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

    /// <summary>
    /// Returns boolean indicating whether budget with the given userId and name exists.
    /// </summary>
    Task<bool> DoesBudgetNameExistAsync(int userId, string? name);
}
