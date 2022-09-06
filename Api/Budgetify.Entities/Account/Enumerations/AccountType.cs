namespace Budgetify.Entities.Account.Enumerations;

using System.Linq;

using Budgetify.Common.Results;
using Budgetify.Entities.Common.Enumerations;

public sealed class AccountType : Enumeration
{
    public static readonly AccountType Cash = new(1, "CASH");
    public static readonly AccountType Card = new(2, "DEBIT");
    public static readonly AccountType Credit = new(3, "CREDIT");
    public static readonly AccountType Savings = new(4, "SAVINGS");

    public AccountType(int id, string name) : base(id, name) { }

    public static Result<AccountType> Create(string? type)
    {
        AccountType? accountType = GetAll<AccountType>().SingleOrDefault(x => x.Name == type);

        if (accountType is null)
        {
            return Result.Invalid<AccountType>(ResultCodes.AccountTypeInvalid);
        }

        return Result.Ok(accountType);
    }

    public static implicit operator string(AccountType type) => type.Name;
}
