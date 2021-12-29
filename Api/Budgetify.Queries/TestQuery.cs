namespace Budgetify.Queries
{
    using System;
    using System.Threading.Tasks;

    using Budgetify.Queries.Infrastructure.Context;

    using Microsoft.EntityFrameworkCore;

    using VS.Queries;

    using TestEntity = Test.Entities.Test;

    public record TestQuery(Guid Uid) : IQuery<TestEntity>;

    public class TestQueryHandler : IQueryHandler<TestQuery, TestEntity>
    {
        private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;

        public TestQueryHandler(IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext)
        {
            _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        }

        public async Task<QueryResult<TestEntity>> ExecuteAsync(TestQuery query)
        {
            QueryResultBuilder<TestEntity> result = new();

            TestEntity test = await _budgetifyReadonlyDbContext
                .AllNoTrackedOf<TestEntity>()
                .SingleOrDefaultAsync(x => x.Uid == query.Uid);

            if (test is null)
            {
                return result.FailWith("ERROR");
            }

            result.SetValue(test);

            return result.Build();
        }
    }
}
