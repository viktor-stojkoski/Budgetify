namespace Budgetify.Storage.ExchangeRate.Entities;

using System;
using System.Collections.Generic;

using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.Currency.Entities;
using Budgetify.Storage.User.Entities;

using VS.DomainEvents;

public class ExchangeRate : AggregateRoot
{
    protected internal ExchangeRate() { }

    protected internal ExchangeRate(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        IEnumerable<IDomainEvent> domainEvents,
        int userId,
        int fromCurrencyId,
        int toCurrencyId,
        DateTime? fromDate,
        DateTime? toDate,
        decimal rate) : base(id, uid, createdOn, deletedOn, domainEvents)
    {
        UserId = userId;
        FromCurrencyId = fromCurrencyId;
        ToCurrencyId = toCurrencyId;
        FromDate = fromDate;
        ToDate = toDate;
        Rate = rate;
    }

    public int UserId { get; protected internal set; }

    public int FromCurrencyId { get; protected internal set; }

    public int ToCurrencyId { get; protected internal set; }

    public DateTime? FromDate { get; protected internal set; }

    public DateTime? ToDate { get; protected internal set; }

    public decimal Rate { get; protected internal set; }

    public virtual User User { get; protected internal set; } = null!;

    public virtual Currency FromCurrency { get; protected internal set; } = null!;

    public virtual Currency ToCurrency { get; protected internal set; } = null!;
}
