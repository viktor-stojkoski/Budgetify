namespace Budgetify.Storage.User.Entities
{
    using System;

    using Budgetify.Storage.Common.Entities;

    public class User : AggregateRoot
    {
        public User(
            int id,
            Guid uid,
            DateTime createdOn,
            DateTime? deletedOn,
            string name,
            string email) : base(id, uid, createdOn, deletedOn)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; protected internal set; }

        public string Email { get; protected internal set; }
    }
}
