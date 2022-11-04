namespace Budgetify.Queries.Merchant.Queries.GetMerchant;

using System;

public class MerchantResponse
{
    public MerchantResponse(string name, Guid categoryUid, string categoryName)
    {
        Name = name;
        CategoryUid = categoryUid;
        CategoryName = categoryName;
    }

    public Guid CategoryUid { get; set; }

    public string Name { get; set; }

    public string CategoryName { get; set; }
}
