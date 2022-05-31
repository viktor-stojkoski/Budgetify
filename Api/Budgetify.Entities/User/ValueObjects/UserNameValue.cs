namespace Budgetify.Entities.User.ValueObjects
{
    using System.Collections.Generic;

    using Budgetify.Common.Extensions;
    using Budgetify.Common.Results;
    using Budgetify.Entities.Common.ValueObjects;

    public sealed class UserNameValue : ValueObject
    {
        private const uint MAX_NAME_LENGTH = 255;

        public string Value { get; }

        private UserNameValue(string value)
        {
            Value = value;
        }

        public static Result<UserNameValue> Create(string? value)
        {
            if (value.IsEmpty())
            {
                return Result.Invalid<UserNameValue>(ResultCodes.UserNameInvalid);
            }

            if (value.Length > MAX_NAME_LENGTH)
            {
                return Result.Invalid<UserNameValue>(ResultCodes.UserNameInvalidLength);
            }

            return Result.Ok(new UserNameValue(value));
        }

        //public static implicit operator string?(UserNameValue? obj) => obj?.Value;

        //public static explicit operator UserNameValue?(string? value) => Of(value);

        //private static UserNameValue? Of(string? value) => (UserNameValue?)(Create(value)?.Value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
