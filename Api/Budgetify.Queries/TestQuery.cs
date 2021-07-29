namespace Budgetify.Queries
{
    using System.Threading.Tasks;

    using Budgetify.Common.Queries;

    public record TestQuery(string Value) : IQuery<string>;

    public class TestQueryHandler : IQueryHandler<TestQuery, string>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<QueryResult<string>> ExecuteAsync(TestQuery query)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            QueryResultBuilder<string> result = new();

            return result.FailWith("ERROR QUERY");

            //result.SetValue(query.Value);

            //return result.Build();
        }
    }
}
