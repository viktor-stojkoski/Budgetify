namespace Budgetify.Common.Queries
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler for query.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query to be executed.</typeparam>
    /// <typeparam name="TResult">Query return type.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles the given query.
        /// </summary>
        /// <param name="query">Query to be handled.</param>
        /// <returns>The query return value.</returns>
        Task<QueryResult<TResult>> ExecuteAsync(TQuery query);
    }
}
