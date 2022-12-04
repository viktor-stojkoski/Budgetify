namespace Budgetify.Entities.Transaction.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.Enumerations;

public partial class Transaction
{
    /// <summary>
    /// Create transaction DB to domain only.
    /// </summary>
    public static Result<Transaction> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        int accountId,
        int categoryId,
        int currencyId,
        int? merchantId,
        string type,
        decimal amount,
        DateTime date,
        string? description)
    {
        Result<TransactionType> typeValue = TransactionType.Create(type);

        if (typeValue.IsFailureOrNull)
        {
            return Result.FromError<Transaction>(typeValue);
        }

        if (typeValue.Value != TransactionType.Income && merchantId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionEmptyMerchantTypeInvalid);
        }

        return Result.Ok(
            new Transaction(
                userId: userId,
                accountId: accountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: typeValue.Value,
                amount: amount,
                date: date,
                description: description)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates transaction.
    /// </summary>
    public static Result<Transaction> Create(
        DateTime createdOn,
        int userId,
        int accountId,
        int categoryId,
        int currencyId,
        int? merchantId,
        string? type,
        decimal amount,
        DateTime date,
        string? description)
    {
        Result<TransactionType> typeValue = TransactionType.Create(type);

        if (typeValue.IsFailureOrNull)
        {
            return Result.FromError<Transaction>(typeValue);
        }

        if (typeValue.Value != TransactionType.Income && merchantId is null)
        {
            return Result.Invalid<Transaction>(ResultCodes.TransactionEmptyMerchantTypeInvalid);
        }

        return Result.Ok(
            new Transaction(
                userId: userId,
                accountId: accountId,
                categoryId: categoryId,
                currencyId: currencyId,
                merchantId: merchantId,
                type: typeValue.Value,
                amount: amount,
                date: date,
                description: description)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
