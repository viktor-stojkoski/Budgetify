namespace Budgetify.Entities.User.ValueObjects
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Budgetify.Common.Extensions;
    using Budgetify.Common.Results;
    using Budgetify.Entities.Common.ValueObjects;

    public sealed class EmailValue : ValueObject
    {
        private const uint MAX_EMAIL_LENGTH = 255;
        private const string EMAIL_REGEX_PATTERN = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        public string Value { get; }

        private EmailValue(string value)
        {
            Value = value;
        }

        public static Result<EmailValue> Create(string? value)
        {
            if (value.IsEmpty())
            {
                return Result.Invalid<EmailValue>(ResultCodes.EmailInvalid);
            }

            value = value.Trim();

            if (value.Length > MAX_EMAIL_LENGTH)
            {
                return Result.Invalid<EmailValue>(ResultCodes.EmailInvalidLength);
            }

            Regex emailRegex = new(EMAIL_REGEX_PATTERN);

            if (emailRegex.IsMatch(value))
            {
                return Result.Ok(new EmailValue(value));
            }

            return Result.Invalid<EmailValue>(ResultCodes.EmailInvalid);
        }

        public static implicit operator string?(EmailValue? obj) => obj?.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
