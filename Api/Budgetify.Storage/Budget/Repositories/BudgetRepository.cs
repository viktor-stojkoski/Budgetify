namespace Budgetify.Storage.Budget.Repositories;

using Budgetify.Contracts.Budget.Repositories;
using Budgetify.Entities.Budget.Domain;
using Budgetify.Storage.Budget.Factories;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;

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
}
