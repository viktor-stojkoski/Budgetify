namespace Budgetify.Entities.ExchangeRate.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.ExchangeRate.ValueObjects;

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
        DateTime? fromDate,
        DateTime? toDate,
        decimal rate)
    {
        Result<ExchangeRateDateRangeValue> dateRangeValue = ExchangeRateDateRangeValue.Create(fromDate, toDate);

        if (dateRangeValue.IsFailureOrNull)
        {
            return Result.FromError<ExchangeRate>(dateRangeValue);
        }

        if (fromCurrencyId == toCurrencyId)
        {
            return Result.Invalid<ExchangeRate>(ResultCodes.ExchangeRateFromAndToCurrencyCannotBeEqual);
        }

        return Result.Ok(
            new ExchangeRate(
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                dateRange: dateRangeValue.Value,
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
        DateTime? fromDate,
        DateTime? toDate,
        decimal rate)
    {
        Result<ExchangeRateDateRangeValue> dateRangeValue = ExchangeRateDateRangeValue.Create(fromDate, toDate);

        if (dateRangeValue.IsFailureOrNull)
        {
            return Result.FromError<ExchangeRate>(dateRangeValue);
        }

        if (fromCurrencyId == toCurrencyId)
        {
            return Result.Invalid<ExchangeRate>(ResultCodes.ExchangeRateFromAndToCurrencyCannotBeEqual);
        }

        return Result.Ok(
            new ExchangeRate(
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                dateRange: dateRangeValue.Value,
                rate: rate)
            {
                Uid = Guid.NewGuid(),
                CreatedOn = createdOn,
                State = EntityState.Added
            });
    }
}
