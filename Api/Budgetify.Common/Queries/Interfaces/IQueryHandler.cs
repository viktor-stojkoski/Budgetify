namespace Budgetify.Common.Queries
{
    using System.Threading.Tasks;

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
