namespace Budgetify.Contracts.Infrastructure.Storage
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        /// <summary>
        /// Persists changes to storage.
        /// </summary>
        Task SaveAsync();
    }
}
