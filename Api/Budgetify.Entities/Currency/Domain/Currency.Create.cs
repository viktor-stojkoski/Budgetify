namespace Budgetify.Entities.Currency.Domain;

using System;

using Budgetify.Common.Results;
using Budgetify.Entities.Currency.ValueObjects;

public partial class Currency
{
    /// <summary>
    /// Create currency DB to domain only.
    /// </summary>
    public static Result<Currency> Create(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        string name,
        string code,
        string? symbol)
    {
        Result<CurrencyNameValue> nameValue = CurrencyNameValue.Create(name);
        Result<CurrencyCodeValue> codeValue = CurrencyCodeValue.Create(code);
        Result<CurrencySymbolValue> symbolValue = CurrencySymbolValue.Create(symbol);

        Result okOrError =
            Result.FirstFailureNullOrOk(
                nameValue,
                codeValue,
                symbolValue);

        if (okOrError.IsFailureOrNull)
        {
            return Result.FromError<Currency>(okOrError);
        }

        return Result.Ok(
            new Currency(
                name: nameValue.Value,
                code: codeValue.Value,
                symbol: symbolValue.Value)
            {
                Id = id,
                Uid = uid,
                CreatedOn = createdOn,
                DeletedOn = deletedOn
            });
    }
}
