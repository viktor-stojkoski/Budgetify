namespace Budgetify.Entities.ExchangeRate.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.DomainEvents;
using Budgetify.Entities.ExchangeRate.ValueObjects;

public partial class ExchangeRate
{
    /// <summary>
    /// Updates exchange rate.
    /// </summary>
    public Result Update(DateTime? fromDate, decimal rate)
    {
        if (DateRange.ToDate.HasValue)
        {
            return Result.Invalid<ExchangeRate>(ResultCodes.ExchangeRateClosed);
        }

        Result<ExchangeRateDateRangeValue> dateRangeResult =
            ExchangeRateDateRangeValue.Create(fromDate, DateRange.ToDate);

        if (dateRangeResult.IsFailureOrNull)
        {
            return Result.FromError<ExchangeRate>(dateRangeResult);
        }

        AddDomainEvent(new ExchangeRateUpdatedDomainEvent(UserId, Uid, Rate));

        DateRange = dateRangeResult.Value;
        Rate = rate;

        MarkModify();

        return Result.Ok();
    }

    /// <summary>
    /// Closes exchange rate.
    /// </summary>
    public Result Close(DateTime closedOn)
    {
        if (DateRange.ToDate.HasValue)
        {
            return Result.Ok();
        }

        Result<ExchangeRateDateRangeValue> dateRangeResult =
            ExchangeRateDateRangeValue.Create(DateRange.FromDate, closedOn);

        if (dateRangeResult.IsFailureOrNull)
        {
            return Result.FromError<ExchangeRate>(dateRangeResult);
        }

        DateRange = dateRangeResult.Value;

        MarkModify();

        return Result.Ok();
    }
}
