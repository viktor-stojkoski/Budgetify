namespace Budgetify.Entities.ExchangeRate.Domain;
using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;

public partial class ExchangeRate
{
    /// <summary>
    /// Creates exchange rate DB to domain only.
    /// </summary>
    public static Result<ExchangeRate> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        int userId,
        int fromCurrencyId,
        int toCurrencyId,
        decimal rate)
    {
        if (fromCurrencyId == toCurrencyId)
        {
            return Result.Invalid<ExchangeRate>(ResultCodes.ExchangeRateFromAndToCurrencyCannotBeEqual);
        }

        return Result.Ok(
            new ExchangeRate(
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                rate: rate)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }

    /// <summary>
    /// Creates exchange rate.
    /// </summary>
    public static Result<ExchangeRate> Create(
        DateTime createdOn,
        int userId,
        int fromCurrencyId,
        int toCurrencyId,
        decimal rate)
    {
        if (fromCurrencyId == toCurrencyId)
        {
            return Result.Invalid<ExchangeRate>(ResultCodes.ExchangeRateFromAndToCurrencyCannotBeEqual);
        }

        return Result.Ok(
            new ExchangeRate(
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                rate: rate)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
