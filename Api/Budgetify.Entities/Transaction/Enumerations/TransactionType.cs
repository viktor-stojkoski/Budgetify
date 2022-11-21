namespace Budgetify.Entities.Transaction.Enumerations;

using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;

public sealed class TransactionType : Enumeration
{
    public static readonly TransactionType Expense = new(1, "EXPENSE");
    public static readonly TransactionType Income = new(2, "INCOME");
    public static readonly TransactionType Transfer = new(3, "TRANSFER");

    public TransactionType(int id, string name) : base(id, name) { }

    public static Result<TransactionType> Create(string? type)
    {
        TransactionType? transactionType = GetAll<TransactionType>().SingleOrDefault(x => x.Name == type);

        if (transactionType is null)
        {
            return Result.Invalid<TransactionType>(ResultCodes.TransactionTypeInvalid);
        }

        return Result.Ok(transactionType);
    }

    public static implicit operator string(TransactionType type) => type.Name;
}
