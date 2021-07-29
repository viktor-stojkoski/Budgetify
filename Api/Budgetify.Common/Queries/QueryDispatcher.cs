namespace Budgetify.Common.Queries
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [DisplayName("Query: {0}")]
        public async Task<QueryResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query)
        {
            Type? handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic? handler = _serviceProvider.GetService(handlerType);

            return await handler?.ExecuteAsync((dynamic)query);
        }
    }
}
