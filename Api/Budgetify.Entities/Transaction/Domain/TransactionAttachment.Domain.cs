namespace Budgetify.Entities.Transaction.Domain;

using System;

using Budgetify.Common.Results;

public partial class TransactionAttachment
{
    /// <summary>
    /// Marks transaction attachment as deleted.
    /// </summary>
    public Result Delete(DateTime deletedOn)
    {
        if (DeletedOn is not null)
        {
            return Result.Ok();
        }

        DeletedOn = deletedOn;

        MarkModify();

        return Result.Ok();
    }
}
