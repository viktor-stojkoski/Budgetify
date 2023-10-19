namespace Budgetify.Entities.Transaction.Domain;

using System;
using System.Collections.Generic;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Transaction.DomainEvents;
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
        int? accountId,
        int? categoryId,
        int currencyId,
        int? merchantId,
        string type,
        decimal amount,
        DateTime? date,
        string? description,
        bool isVerified,
        IEnumerable<TransactionAttachment> attachments)
    {
        Result<TransactionType> typeValue = TransactionType.Create(type);

        if (typeValue.IsFailureOrNull)
        {
            return Result.FromError<Transaction>(typeValue);
        }

        Transaction transaction = new(
            userId: userId,
            accountId: accountId,
            categoryId: categoryId,
            currencyId: currencyId,
            merchantId: merchantId,
            type: typeValue.Value,
            amount: amount,
            date: date,
            description: description,
            isVerified: isVerified)
        {
            Id = id,
            Uid = uid,
            CreatedOn = createdOn,
            DeletedOn = deletedOn
        };

        transaction._attachments.AddRange(
            attachments ?? Enumerable.Empty<TransactionAttachment>());

        return Result.Ok(transaction);
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

        Transaction transaction = new(
            userId: userId,
            accountId: accountId,
            categoryId: categoryId,
            currencyId: currencyId,
            merchantId: merchantId,
            type: typeValue.Value,
            amount: amount,
            date: date,
            description: description,
            isVerified: true)
        {
            Uid = Guid.NewGuid(),
            CreatedOn = createdOn,
            State = EntityState.Added
        };

        transaction.AddDomainEvent(
            new TransactionCreatedDomainEvent(
                UserId: transaction.UserId,
                TransactionUid: transaction.Uid,
                TransactionType: transaction.Type));

        return Result.Ok(transaction);
    }

    /// <summary>
    /// Creates transaction by scan.
    /// </summary>
    public static Result<Transaction> CreateByScan(
        DateTime createdOn,
        int userId,
        int? merchantId,
        int currencyId,
        decimal amount,
        DateTime? date)
    {
        return Result.Ok(
            new Transaction(
                userId: userId,
                accountId: null,
                categoryId: null,
                currencyId: currencyId,
                merchantId: merchantId,
                type: TransactionType.Expense,
                amount: amount,
                date: date,
                description: null,
                isVerified: false)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
