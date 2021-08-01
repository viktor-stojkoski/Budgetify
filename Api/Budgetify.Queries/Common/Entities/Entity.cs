namespace Budgetify.Queries.Common.Entities
{
    using System;

    public abstract class Entity
    {
        /// <summary>
        /// Entity's database id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Entity's unique identifier.
        /// </summary>
        public Guid Uid { get; set; }

        /// <summary>
        /// Entity's date and time of creation.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Entity's date and time of deletion.
        /// </summary>
        public DateTime? DeletedOn { get; set; }

        protected Entity() { }
    }
}
