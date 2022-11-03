namespace Budgetify.Queries.Merchant.Queries.GetMerchant;

public class MerchantResponse
{
    public MerchantResponse(string name, string categoryName)
    {
        Name = name;
        CategoryName = categoryName;
    }

    public string Name { get; set; }

    public string CategoryName { get; set; }
}
