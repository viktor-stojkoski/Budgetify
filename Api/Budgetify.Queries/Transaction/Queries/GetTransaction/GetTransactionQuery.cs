namespace Budgetify.Queries.Transaction.Queries.GetTransaction;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Budgetify.Common.CurrentUser;
using Budgetify.Common.Results;
using Budgetify.Common.Storage;
using Budgetify.Contracts.Settings;
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
    private readonly IStorageService _storageService;
    private readonly IStorageSettings _storageSettings;

    public GetTransactionQueryHandler(
        IBudgetifyReadonlyDbContext budgetifyReadonlyDbContext,
        ICurrentUser currentUser,
        IStorageService storageService,
        IStorageSettings storageSettings)
    {
        _budgetifyReadonlyDbContext = budgetifyReadonlyDbContext;
        _currentUser = currentUser;
        _storageService = storageService;
        _storageSettings = storageSettings;
    }

    public async Task<QueryResult<TransactionResponse>> ExecuteAsync(GetTransactionQuery query)
    {
        QueryResultBuilder<TransactionResponse> result = new();

        Transaction? transaction =
            await _budgetifyReadonlyDbContext.AllNoTrackedOf<Transaction>()
                .Include(x => x.Account)
                .Include(x => x.Category)
                .Include(x => x.Currency)
                .Include(x => x.Merchant).DefaultIfEmpty()
                .Include(x => x.TransactionAttachments)
                .Where(x => x.UserId == _currentUser.Id && x.Uid == query.TransactionUid)
                .SingleOrDefaultAsync();

        if (transaction is null)
        {
            return result.FailWith(Result.NotFound(ResultCodes.TransactionNotFound));
        }

        List<Task<SignedUrlResponse>> attachmentsTasks = new();

        foreach (TransactionAttachment attachment in transaction.TransactionAttachments)
        {
            attachmentsTasks.Add(
                _storageService.GetSignedUrlAsync(
                    containerName: _storageSettings.ContainerName,
                    fileName: attachment.FilePath,
                    expiresOn: DateTime.UtcNow.AddHours(3)));
        }

        SignedUrlResponse[] signedUrlResponses = await Task.WhenAll(attachmentsTasks);

        TransactionResponse transactionResponse = new(
            accountUid: transaction.Account.Uid,
            accountName: transaction.Account.Name,
            categoryUid: transaction.Category.Uid,
            categoryName: transaction.Category.Name,
            currencyCode: transaction.Currency.Code,
            merchantUid: transaction.Merchant?.Uid,
            merchantName: transaction.Merchant?.Name,
            type: transaction.Type,
            amount: transaction.Amount,
            date: transaction.Date,
            description: transaction.Description,
            transactionAttachments: transaction.TransactionAttachments.Select(
                attachment => new TransactionAttachmentResponse(
                    uid: attachment.Uid,
                    createdOn: attachment.CreatedOn,
                    name: attachment.Name,
                    url: signedUrlResponses.Single(x => x.FileName == attachment.FilePath).Url)));

        result.SetValue(transactionResponse);

        return result.Build();
    }
}
