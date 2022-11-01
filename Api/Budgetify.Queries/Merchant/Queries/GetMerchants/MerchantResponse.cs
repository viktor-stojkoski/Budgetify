namespace Budgetify.Queries.Merchant.Queries.GetMerchants;

using System;

public class MerchantResponse
{
    public MerchantResponse(Guid uid, string name, string categoryName)
    {
        Uid = uid;
        Name = name;
        CategoryName = categoryName;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string CategoryName { get; set; }
}
