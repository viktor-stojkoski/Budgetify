namespace Budgetify.Common.Queries
{
    using System.Threading.Tasks;

    public interface IQueryDispatcher
    {
        /// <summary>
        /// Executes the given query.
        /// </summary>
        /// <typeparam name="TResult">Return value type.</typeparam>
        /// <param name="query">Query to be executed.</param>
        /// <returns>The query return value.</returns>
        Task<QueryResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}
