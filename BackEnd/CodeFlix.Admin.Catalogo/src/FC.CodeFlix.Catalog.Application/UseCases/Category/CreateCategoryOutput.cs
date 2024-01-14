using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category;

public record CreateCategoryOutput
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; }
    public DateTime CreatedAt { get; }

    public CreateCategoryOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public static implicit operator CreateCategoryOutput(DomainEntity.Category category)
    {
        return new CreateCategoryOutput(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
    }
}