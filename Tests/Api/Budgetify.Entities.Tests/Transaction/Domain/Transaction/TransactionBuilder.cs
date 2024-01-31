namespace Budgetify.Entities.Tests.Transaction.Domain.Transaction;

using System;

using Budgetify.Entities.Transaction.Domain;

internal class TransactionBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 31);
    private readonly DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly int _accountId = 3;
    private readonly int? _fromAccountId = null;
    private readonly int _categoryId = 4;
    private readonly int? _currencyId = 5;
    private readonly int? _merchantId = 6;
    private readonly string _type = "EXPENSE";
    private readonly decimal _amount = 1000;
    private readonly DateTime _date = new(2024, 1, 31);
    private readonly string _description = "Test description";
    private readonly bool _isVerified = true;

    internal Transaction Build()
    {
        return Transaction.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            accountId: _accountId,
            fromAccountId: _fromAccountId,
            categoryId: _categoryId,
            currencyId: _currencyId,
            merchantId: _merchantId,
            type: _type,
            amount: _amount,
            date: _date,
            description: _description,
            isVerified: _isVerified,
            )
    }
}
