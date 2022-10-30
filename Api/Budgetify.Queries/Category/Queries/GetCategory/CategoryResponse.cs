namespace Budgetify.Queries.Category.Queries.GetCategory;

public class CategoryResponse
{
    public CategoryResponse(string name, string type)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; set; }

    public string Type { get; set; }
}
