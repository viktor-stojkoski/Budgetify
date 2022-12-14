namespace Budgetify.Entities.ExchangeRate.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.ExchangeRate.ValueObjects;

public partial class ExchangeRate
{
    /// <summary>
    /// Closes exchange rate.
    /// </summary>
    public Result Close(DateTime closedOn)
    {
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
