﻿namespace Budgetify.Queries.Account.Queries.GetAccounts;

using System;

public class AccountResponse
{
    public AccountResponse(
        Guid uid,
        string name,
        string type,
        decimal balance,
        string currencyCode,
        string? description)
    {
        Uid = uid;
        Name = name;
        Type = type;
        Balance = balance;
        CurrencyCode = currencyCode;
        Description = description;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal Balance { get; set; }

    public string CurrencyCode { get; set; }

    public string? Description { get; set; }
}
