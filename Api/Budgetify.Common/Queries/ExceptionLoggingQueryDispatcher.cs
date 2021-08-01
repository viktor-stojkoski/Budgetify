namespace Budgetify.Common.Queries
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    public class ExceptionLoggingQueryDispatcher : IQueryDispatcher
    {
        private readonly IQueryDispatcher _inner;
        private readonly ILogger<ExceptionLoggingQueryDispatcher> _logger;

        public ExceptionLoggingQueryDispatcher(
            IQueryDispatcher inner,
            ILogger<ExceptionLoggingQueryDispatcher> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<QueryResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            try
            {
                return await _inner.ExecuteAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing query {QueryName} - (@{Query})",
                    query.GetType().Name, query);

                return new QueryResultBuilder<TResult>().FailWith(ex.Message);
            }
        }
    }
}
