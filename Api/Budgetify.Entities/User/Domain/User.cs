namespace Budgetify.Entities.User.Domain
{
    using Budgetify.Entities.Common.Entities;
    using Budgetify.Entities.Common.Enumerations;
    using Budgetify.Entities.User.ValueObjects;

    public sealed partial class User : AggregateRoot
    {
        private User() => State = EntityState.Unchanged;

        /// <summary>
        /// User's name.
        /// </summary>
        public UserNameValue? Name { get; private set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        public EmailValue? Email { get; private set; }
    }
}
