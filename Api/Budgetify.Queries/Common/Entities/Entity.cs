namespace Budgetify.Queries.Common.Entities
{
    using System;

    public abstract class Entity
    {
        public int Id { get; set; }

        public Guid Uid { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        protected Entity() { }
    }
}
