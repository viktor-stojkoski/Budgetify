namespace Budgetify.Storage.User.Entities
{
    using Budgetify.Storage.Common.Entities;

    public class User : AggregateRoot
    {
        public string? Name { get; protected internal set; }

        public string? Email { get; protected internal set; }
    }
}
