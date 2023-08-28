namespace Budgetify.Storage.Budget.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Storage.Budget.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

public class BudgetRepository : Repository<Entities.Budget>, IBudgetRepository
{
    public BudgetRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(Budget budget)
    {
        Entities.Budget dbBudget = budget.CreateBudget();

        Insert(dbBudget);
    }

    public void Update(Budget budget)
    {
        Entities.Budget dbBudget = budget.CreateBudget();

        AttachOrUpdate(dbBudget, budget.State.GetState());
    }

    public async Task<bool> DoesBudgetNameExistAsync(int userId, string? name)
    {
        return await AllNoTrackedOf<Entities.Budget>()
            .AnyAsync(x => x.UserId == userId && x.Name == name);
    }

    public async Task<Result<Budget>> GetBudgetAsync(int userId, Guid budgetUid)
    {
        Entities.Budget? dbBudget = await AllNoTrackedOf<Entities.Budget>()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.Uid == budgetUid);

        if (dbBudget is null)
        {
            return Result.NotFound<Budget>(ResultCodes.BudgetNotFound);
        }

        return dbBudget.CreateBudget();
    }

    public async Task<Result<IEnumerable<Budget>>> GetBudgetsByCategoryIdAsync(int userId, int categoryId)
    {
        IEnumerable<Entities.Budget> dbBudgets = await AllNoTrackedOf<Entities.Budget>()
            .Where(x => x.UserId == userId && x.CategoryId == categoryId)
            .ToListAsync();

        IEnumerable<Result<Budget>> dbBudgetsResults = dbBudgets.CreateBudgets();

        Result dbBudgetsResult = Result.FirstFailureNullOrOk(dbBudgetsResults);

        if (dbBudgetsResult.IsFailureOrNull)
        {
            return Result.FromError<IEnumerable<Budget>>(dbBudgetsResult);
        }

        return Result.Ok(dbBudgetsResults.Select(x => x.Value));
    }
}
