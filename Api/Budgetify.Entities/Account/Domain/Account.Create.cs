namespace Budgetify.Entities.Account.Domain;
using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Account.Enumerations;
using Budgetify.Entities.Account.ValueObjects;
using Budgetify.Entities.Common.Enumerations;

public partial class Account
{
    /// <summary>
    /// Create account DB to domain only.
    /// </summary>
    public static Result<Account> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        string name,
        string type,
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

        return Result.Ok(
            new Account(
                userId: userId,
                name: nameValue.Value,
                type: typeValue.Value,
                balance: balance,
                currencyId: currencyId,
                description: description)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates account.
    /// </summary>
    public static Result<Account> Create(
        DateTime createdOn,
        int userId,
        string name,
        string type,
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

        return Result.Ok(
            new Account(
                userId: userId,
                name: nameValue.Value,
                type: typeValue.Value,
                balance: balance,
                currencyId: currencyId,
                description: description)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
