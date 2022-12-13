namespace Budgetify.Entities.ExchangeRate.ValueObjects;

using System;
using System.Collections.Generic;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class ExchangeRateDateRangeValue : ValueObject
{
    public DateTime? FromDate { get; }

    public DateTime? ToDate { get; }

    private ExchangeRateDateRangeValue(DateTime? fromDate, DateTime? toDate)
    {
        FromDate = fromDate;
        ToDate = toDate;
    }

    public static Result<ExchangeRateDateRangeValue> Create(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate.HasValue && toDate.HasValue && fromDate >= toDate)
        {
            return Result.Invalid<ExchangeRateDateRangeValue>(ResultCodes.ExchangeRateFromDateCannotBeGreaterThanToDate);
        }

        return Result.Ok(new ExchangeRateDateRangeValue(fromDate, toDate));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if (!FromDate.HasValue && !ToDate.HasValue)
        {
            yield break;
        }

        if (!FromDate.HasValue && ToDate.HasValue)
        {
            yield return ToDate;
        }

        if (!ToDate.HasValue && FromDate.HasValue)
        {
            yield return FromDate;
        }

        if (ToDate.HasValue && FromDate.HasValue)
        {
            yield return FromDate;
            yield return ToDate;
        }
    }
}
