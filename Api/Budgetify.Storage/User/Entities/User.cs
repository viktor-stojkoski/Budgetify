﻿namespace Budgetify.Storage.User.Entities;

using System;
using System.Collections.Generic;

using Budgetify.Storage.Account.Entities;
using Budgetify.Storage.Budget.Entities;
using Budgetify.Storage.Category.Entities;
using Budgetify.Storage.Common.Entities;
using Budgetify.Storage.ExchangeRate.Entities;
using Budgetify.Storage.Merchant.Entities;
using Budgetify.Storage.Transaction.Entities;

public class User : AggregateRoot
{
    public User(
        int id,
        Guid uid,
        DateTime createdOn,
        DateTime? deletedOn,
        string email,
        string firstName,
        string lastName,
        string city) : base(id, uid, createdOn, deletedOn)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        City = city;
    }

    public string Email { get; protected internal set; }

    public string FirstName { get; protected internal set; }

    public string LastName { get; protected internal set; }

    public string City { get; protected internal set; }

    public virtual ICollection<Account> Accounts { get; protected internal set; } = new List<Account>();

    public virtual ICollection<Category> Categories { get; protected internal set; } = new List<Category>();

    public virtual ICollection<Merchant> Merchants { get; protected internal set; } = new List<Merchant>();

    public virtual ICollection<Transaction> Transactions { get; protected internal set; } = new List<Transaction>();

    public virtual ICollection<ExchangeRate> ExchangeRates { get; protected internal set; } = new List<ExchangeRate>();

    public virtual ICollection<Budget> Budgets { get; protected internal set; } = new List<Budget>();
}
