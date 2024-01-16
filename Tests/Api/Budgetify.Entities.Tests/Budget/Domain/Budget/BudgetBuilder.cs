namespace Budgetify.Entities.Tests.Budget.Domain.Budget;

using System;

using Budgetify.Entities.Budget.Domain;

internal class BudgetBuilder
{
    private readonly int _id = 1;
    private readonly Guid _uid = Guid.NewGuid();
    private readonly DateTime _createdOn = new(2024, 1, 16);
    private readonly DateTime? _deletedOn = null;
    private readonly int _userId = 2;
    private readonly string _name = "Name";
    private readonly int _categoryId = 3;
    private readonly int _currencyId = 4;
    private readonly DateTime _startDate = new(2024, 1, 1);
    private readonly DateTime _endDate = new(2024, 2, 1);
    private readonly decimal _amount = 10000;
    private readonly decimal _amountSpent = 1000;

    internal Budget Build()
    {
        return Budget.Create(
            id: _id,
            uid: _uid,
            createdOn: _createdOn,
            deletedOn: _deletedOn,
            userId: _userId,
            name: _name,
            categoryId: _categoryId,
            currencyId: _currencyId,
            startDate: _startDate,
            endDate: _endDate,
            amount: _amount,
            amountSpent: _amountSpent).Value;
    }
}
