namespace Budgetify.Contracts.Budget.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Budgetify.Common.Results;
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

    /// <summary>
    /// Returns budget by given userId and budgetUid.
    /// </summary>
    Task<Result<Budget>> GetBudgetAsync(int userId, Guid budgetUid);

    /// <summary>
    /// Returns the budgets by given categoryId.
    /// </summary>
    Task<Result<IEnumerable<Budget>>> GetBudgetsByCategoryIdAsync(int userId, int categoryId);
}
