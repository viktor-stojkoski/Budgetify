namespace Budgetify.Entities.Tests.ExchangeRate.Domain.ExchangeRate;

using System;

using Budgetify.Entities.ExchangeRate.Domain;

internal class ExchangeRateBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 19);
    private DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly int _fromCurrencyId = 3;
    private readonly int _toCurrencyId = 4;
    private readonly DateTime _fromDate = new(2024, 1, 1);
    private DateTime? _toDate = null;
    private readonly decimal _rate = 10;

    internal ExchangeRate Build()
    {
        return ExchangeRate.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            fromCurrencyId: _fromCurrencyId,
            toCurrencyId: _toCurrencyId,
            fromDate: _fromDate,
            toDate: _toDate,
            rate: _rate).Value;
    }

    internal ExchangeRateBuilder WithToDate(DateTime toDate)
    {
        _toDate = toDate;
        return this;
    }

    internal ExchangeRateBuilder WithDeletedOn(DateTime deletedOn)
    {
        _deletedOn = deletedOn;
        return this;
    }
}
