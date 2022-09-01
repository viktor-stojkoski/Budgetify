namespace Budgetify.Queries.Common.Extensions;

using Budgetify.Common.Results;

using VS.Queries;

public static class QueryExtensions
{
    public static QueryResult<TValue> FailWith<TValue>(
        this QueryResultBuilder<TValue> queryResultBuilder,
        Result result)
    {
        queryResultBuilder.SetMessage(result.Message);
        queryResultBuilder.SetStatusCode(result.HttpStatusCode);

        return queryResultBuilder.Build();
    }
}
