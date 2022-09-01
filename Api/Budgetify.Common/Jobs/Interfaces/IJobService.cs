namespace Budgetify.Common.Jobs;

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IJobService
{
    /// <summary>
    /// Creates a background job based on a specified lambda expression and places it into its queue.
    /// </summary>
    /// <param name="methodCall">Method call expression that will be queued.</param>
    /// <returns>Unique identifier of the created job.</returns>
    string Enqueue(Expression<Action> methodCall);

    /// <summary>
    /// Creates a background job based on a specified lambda expression and places it into its queue.
    /// </summary>
    /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
    /// <param name="methodCall">Method call that will be queued.</param>
    /// <returns>Unique identifier of the created job.</returns>
    string Enqueue<T>(Expression<Func<T, Task>> methodCall);

    /// <summary>
    /// Creates a background job based on a specified lambda expression and places it into its queue.
    /// </summary>
    /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
    /// <param name="methodCall">Method call that will be queued.</param>
    /// <returns>Unique identifier of the created job.</returns>
    string Enqueue<T>(Expression<Action<T>> methodCall);

    /// <summary>
    /// Creates a background job based on a specified lambda expression and places it into its queue.
    /// </summary>
    /// <param name="methodCall">Method call that will be queued.</param>
    /// <returns>Unique identifier of the created job.</returns>
    string Enqueue(Expression<Func<Task>> methodCall);
}
