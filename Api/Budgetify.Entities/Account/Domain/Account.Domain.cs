namespace Budgetify.Entities.Account.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Enumerations;
using Budgetify.Entities.Account.ValueObjects;

public partial class Account
{
    /// <summary>
    /// Updates account.
    /// </summary>
    public Result Update(
        string? name,
        string? type,
        decimal balance,
        int currencyId,
        string? description)
    {
        Result<AccountNameValue> nameValue = AccountNameValue.Create(name);
        Result<AccountType> typeValue = AccountType.Create(type);

        Result okOrError = Result.FirstFailureNullOrOk(nameValue, typeValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<Account>(okOrError);
        }

        Name = nameValue.Value;
        Type = typeValue.Value;
        Balance = balance;
        CurrencyId = currencyId;
        Description = description;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks account as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        DeletedOn = deletedOn;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Deducts the given amount from balance.
    /// </summary>
    public Result DeductBalance(decimal amount)
    {
        Balance -= amount;

        MarkModify();

        return Result.Ok();
    }
}
