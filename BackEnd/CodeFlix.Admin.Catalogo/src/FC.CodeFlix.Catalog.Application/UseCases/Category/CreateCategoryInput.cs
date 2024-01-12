namespace FC.CodeFlix.Catalog.Application.UseCases.Category;

public record CreateCategoryInput
{
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; }

    public CreateCategoryInput(string name, string description, bool isActive)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
    }
}