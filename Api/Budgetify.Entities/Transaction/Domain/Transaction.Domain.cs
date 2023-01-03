namespace Budgetify.Entities.Transaction.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Transaction.DomainEvents;
using Budgetify.Entities.Transaction.Enumerations;

public partial class Transaction
{
    /// <summary>
    /// Updates transaction.
    /// </summary>
    public Result Update(
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

        AddDomainEvent(
            new TransactionUpdatedDomainEvent(
                AccountId: AccountId,
                DifferenceAmount: Amount > amount ? -Math.Abs(Amount - amount) : Math.Abs(Amount - amount)));

        AccountId = accountId;
        CategoryId = categoryId;
        CurrencyId = currencyId;
        MerchantId = merchantId;
        Type = typeValue.Value;
        Amount = amount;
        Date = date;
        Description = description;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Marks transaction as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        DeletedOn = deletedOn;

        MarkModify();

        AddDomainEvent(new TransactionDeletedDomainEvent(AccountId, -Amount));

        return Result.Ok();
    }

    public Result UpdateAmount(decimal amount)
    {
        Amount = amount;

        MarkModify();

        return Result.Ok();
    }
}
