namespace Budgetify.Entities.Tests.ExchangeRate.Domain.ExchangeRate;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.ExchangeRate.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateFromStorageShould))]
public class CreateFromStorageShould
{
    [Test]
    public void WhenDateRangeInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2024, 1, 19);
        int userId = RandomId();
        int fromCurrencyId = RandomId();
        int toCurrencyId = RandomId();
        DateTime fromDate = new(2024, 2, 1);
        DateTime toDate = new(2024, 1, 1);
        decimal rate = 10;

        // Act
        Result<ExchangeRate> result =
            ExchangeRate.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                fromDate: fromDate,
                toDate: toDate,
                rate: rate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.ExchangeRateFromDateCannotBeGreaterThanToDate);
    }

    [Test]
    public void WhenCurrenciesEqual_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2024, 1, 19);
        int userId = RandomId();
        int fromCurrencyId = 5;
        int toCurrencyId = 5;
        DateTime fromDate = new(2024, 1, 1);
        DateTime toDate = new(2024, 2, 1);
        decimal rate = 10;

        // Act
        Result<ExchangeRate> result =
            ExchangeRate.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                fromDate: fromDate,
                toDate: toDate,
                rate: rate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.ExchangeRateFromAndToCurrencyCannotBeEqual);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateExchangeRateFromStorage()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2024, 1, 19);
        int userId = RandomId();
        int fromCurrencyId = RandomId();
        int toCurrencyId = RandomId();
        DateTime fromDate = new(2024, 1, 1);
        DateTime toDate = new(2024, 2, 1);
        decimal rate = 10;

        // Act
        Result<ExchangeRate> result =
            ExchangeRate.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                userId: userId,
                fromCurrencyId: fromCurrencyId,
                toCurrencyId: toCurrencyId,
                fromDate: fromDate,
                toDate: toDate,
                rate: rate);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Unchanged);
        result.Value.Id.Should().Be(id);
        result.Value.Uid.Should().Be(uid);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.UserId.Should().Be(userId);
        result.Value.FromCurrencyId.Should().Be(fromCurrencyId);
        result.Value.ToCurrencyId.Should().Be(toCurrencyId);
        result.Value.DateRange.FromDate.Should().Be(fromDate);
        result.Value.DateRange.ToDate.Should().Be(toDate);
        result.Value.Rate.Should().Be(rate);
    }
}
