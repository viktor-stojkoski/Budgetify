namespace Budgetify.Queries.Test.Entities
{
    using Budgetify.Queries.Common.Entities;

    public class Test : Entity
    {
        public string? Name { get; protected internal set; }

        public string? Address { get; protected internal set; }
    }
}