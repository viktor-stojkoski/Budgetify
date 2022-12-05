namespace Budgetify.Queries.Transaction.Queries.GetTransaction;

using System;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Queries.Common.Extensions;
using Budgetify.Queries.Infrastructure.Context;
using Budgetify.Queries.Transaction.Entities;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetTransactionQuery(Guid TransactionUid) : IQuery<TransactionResponse>;

public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, TransactionResponse>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetTransactionQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<TransactionResponse>> ExecuteAsync(GetTransactionQuery query)
    {
        QueryResultBuilder<TransactionResponse> result = new();

        TransactionResponse? transaction =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Transaction>()
                .Include(x => x.Account)
                .Include(x => x.Category)
                .Include(x => x.Currency)
                .Include(x => x.Merchant).DefaultIfEmpty()
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.TransactionUid)
                .Select(x => new TransactionResponse(
                    x.Account.Name,
                    x.Category.Name,
                    x.Currency.Code,
                    x.Merchant.Name,
                    x.Type,
                    x.Amount,
                    x.Date,
                    x.Description))
                .SingleOrDefaultAsync();

        if (transaction is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.TransactionNotFound));
        }

        result.SetValue(transaction);

        return result.Build();
    }
}
