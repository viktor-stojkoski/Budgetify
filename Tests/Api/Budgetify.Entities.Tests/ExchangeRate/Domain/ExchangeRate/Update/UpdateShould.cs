namespace Budgetify.Entities.Tests.ExchangeRate.Domain.ExchangeRate;

using System;
using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.ExchangeRate.Domain;
using Budgetify.Entities.ExchangeRate.DomainEvents;

using FluentAssertions;

using NUnit.Framework;

[TestFixture(Category = nameof(UpdateShould))]
public class UpdateShould
{
    [Test]
    public void WhenExchangeRateClosed_WillReturnErrorResult()
    {
        // Arrange
        DateTime fromDate = new(2024, 1, 10);
        decimal rate = 100;

        ExchangeRate exchangeRate = new ExchangeRateBuilder()
            .WithToDate(new DateTime(2024, 2, 10))
            .Build();

        // Act
        Result result = exchangeRate.Update(fromDate, rate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.ExchangeRateClosed);

        exchangeRate.State.Should().Be(EntityState.Unchanged);
        exchangeRate.DateRange.FromDate.Should().Be(new DateTime(2024, 1, 1));
        exchangeRate.Rate.Should().Be(10);
    }

    [Test]
    public void WhenArgumentsCorrect_WillUpdateExchangeRate()
    {
        // Arrange
        DateTime? fromDate = new DateTime(2024, 1, 10);
        decimal rate = 100;

        ExchangeRate exchangeRate = new ExchangeRateBuilder()
            .Build();

        // Act
        Result result = exchangeRate.Update(fromDate, rate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        exchangeRate.State.Should().Be(EntityState.Modified);
        exchangeRate.DateRange.FromDate.Should().Be(fromDate);
        exchangeRate.Rate.Should().Be(rate);

        exchangeRate.DomainEvents.Should().HaveCount(1);

        ExchangeRateUpdatedDomainEvent? domainEvent =
            exchangeRate.DomainEvents
                .SingleOrDefault(x => x is ExchangeRateUpdatedDomainEvent)
                    as ExchangeRateUpdatedDomainEvent;

        domainEvent.Should().NotBeNull();
        domainEvent?.UserId.Should().Be(exchangeRate.UserId);
        domainEvent?.ExchangeRateUid.Should().Be(exchangeRate.Uid);
        domainEvent?.PreviousRate.Should().Be(10);
    }
}
