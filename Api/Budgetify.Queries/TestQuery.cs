namespace Budgetify.Queries.Asd
{
    using System;
    using System.Threading.Tasks;

    using Budgetify.Queries.Infrastructure.Context;
    using Budgetify.Queries.User.Entities;

    using Microsoft.EntityFrameworkCore;

    using VS.Queries;

    public record TestQuery(Guid Uid) : IQuery<User>;

    public class TestQueryHandler : IQueryHandler<TestQuery, User>
    {
        private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;

        public TestQueryHandler(IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext)
        {
            _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        }

        public async Task<QueryResult<User>> ExecuteAsync(TestQuery query)
        {
            QueryResultBuilder<User> result = new();

            User? test = await _budgetifyReadonlyDbContext
                .AllNoTrackedOf<User>()
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
