namespace Budgetify.Contracts.Merchant.Requests;

using System;

public class CreateMerchantRequest
{
    public string? Name { get; set; }

    public Guid CategoryUid { get; set; }
}
