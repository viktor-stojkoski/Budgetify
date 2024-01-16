namespace Budgetify.Entities.Budget.ValueObjects;

using System;
using System.Collections.Generic;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.ValueObjects;

public sealed class BudgetDateRangeValue : ValueObject
{
    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    private BudgetDateRangeValue(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<BudgetDateRangeValue> Create(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return Result.Invalid<BudgetDateRangeValue>(ResultCodes.BudgetStartDateCannotBeGreaterThanEndDate);
        }

        return Result.Ok(new BudgetDateRangeValue(startDate, endDate));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}
