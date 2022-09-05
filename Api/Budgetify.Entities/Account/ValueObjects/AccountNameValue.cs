namespace Budgetify.Entities.Account.ValueObjects;

using System.Collections.Generic;

using Budgetify.Common.Extensions;
using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class AccountNameValue : ValueObject
{
    private const uint AccountNameMaxLength = 255;

    public string Value { get; }

    private AccountNameValue(string value)
    {
        Value = value;
    }

    public static Result<AccountNameValue> Create(string? value)
    {
        if (value.IsEmpty())
        {
            return Result.Invalid<AccountNameValue>(ResultCodes.AccountNameInvalid);
        }

        if (value.Length > AccountNameMaxLength)
        {
            return Result.Invalid<AccountNameValue>(ResultCodes.AccountNameInvalidLength);
        }

        return Result.Ok(new AccountNameValue(value));
    }

    public static implicit operator string(AccountNameValue obj) => obj.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
