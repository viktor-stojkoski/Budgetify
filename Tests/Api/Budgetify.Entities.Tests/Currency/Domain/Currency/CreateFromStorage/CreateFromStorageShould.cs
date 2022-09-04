namespace Budgetify.Entities.Tests.Currency.Domain.Currency;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;
using Budgetify.Entities.Currency.Domain;

using FluentAssertions;

using NUnit.Framework;

using static CommonTestsHelper;

[TestFixture(Category = nameof(CreateFromStorageShould))]
public class CreateFromStorageShould
{
    [Test]
    public void WhenNameInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 3);
        string name = RandomString(51);
        string code = "MKD";
        string symbol = "ден";

        // Act
        Result<Currency> result =
            Currency.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                name: name,
                code: code,
                symbol: symbol);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyNameInvalidLength);
    }

    [Test]
    public void WhenCodeInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 4);
        string name = "Macedonian Denar";
        string code = "MK";
        string symbol = "ден";

        // Act
        Result<Currency> result =
            Currency.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                name: name,
                code: code,
                symbol: symbol);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencyCodeInvalid);
    }

    [Test]
    public void WhenSymbolInvalid_WillReturnErrorResult()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 4);
        string name = "Macedonian Denar";
        string code = "MKD";
        string symbol = RandomString(11);

        // Act
        Result<Currency> result =
            Currency.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                name: name,
                code: code,
                symbol: symbol);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Message.Should().Be(ResultCodes.CurrencySymbolInvalidLength);
    }

    [Test]
    public void WhenArgumentsCorrect_WillCreateCurrencyFromStorage()
    {
        // Arrange
        int id = RandomId();
        Guid uid = Guid.NewGuid();
        DateTime createdOn = new(2022, 9, 4);
        string name = "Macedonian Denar";
        string code = "MKD";
        string symbol = "ден";

        // Act
        Result<Currency> result =
            Currency.Create(
                id: id,
                uid: uid,
                createdOn: createdOn,
                deletedOn: null,
                name: name,
                code: code,
                symbol: symbol);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.State.Should().Be(EntityState.Unchanged);
        result.Value.Id.Should().Be(id);
        result.Value.Uid.Should().Be(uid);
        result.Value.CreatedOn.Should().Be(createdOn);
        result.Value.DeletedOn.Should().BeNull();
        result.Value.Name.Value.Should().Be(name);
        result.Value.Code.Value.Should().Be(code);
        result.Value.Symbol.Value.Should().Be(symbol);
    }
}
