namespace Budgetify.Queries.Transaction.Queries.GetTransactions;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Queries.Infrastructure.Context;
using Budgetify.Queries.Transaction.Entities;

using Microsoft.EntityFrameworkCore;

using VS.Queries;

public record GetTransactionsQuery() : IQuery<IEnumerable<TransactionResponse>>;

public class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, IEnumerable<TransactionResponse>>
{
    private readonly IBudgetifyReadonlyDbContext _budgetifyReadonlyDbContext;
    private readonly ICurrentUser _currentUser;

    public GetTransactionsQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
    }

    public async Task<QueryResult<IEnumerable<TransactionResponse>>> ExecuteAsync(GetTransactionsQuery query)
    {
        QueryResultBuilder<IEnumerable<TransactionResponse>> result = new();

        IEnumerable<TransactionResponse> transactions =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Transaction>()
                .Include(x => x.Account).DefaultIfEmpty()
                .Include(x => x.Category).DefaultIfEmpty()
                .Include(x => x.Currency)
                .Include(x => x.Merchant).DefaultIfEmpty()
                .Where(x => x.UserId == _currentUser.Id)
                .Select(x => new TransactionResponse(
                    x.Uid,
                    x.Account == null ? null : x.Account.Name,
                    x.Category == null ? null : x.Category.Name,
                    x.Currency.Code,
                    x.Merchant == null ? null : x.Merchant.Name,
                    x.Type,
                    x.Amount,
                    x.Date,
                    x.Description,
                    x.IsVerified))
                .ToListAsync();

        result.SetValue(transactions);

        return result.Build();
    }
}
