namespace Budgetify.Storage.Test.Entities
{

    using Budgetify.Storage.Common.Entities;

    public class Test : AggregateRoot
    {
        public string? Name { get; protected internal set; }

        public string? Address { get; protected internal set; }
    }
}
