namespace Budgetify.Queries.Category.Queries.GetCategories;

using System;

public class CategoryResponse
{
    public CategoryResponse(Guid uid, string name, string type)
    {
        Uid = uid;
        Name = name;
        Type = type;
    }

    public Guid Uid { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }
}
