using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Common;

/// <summary>
/// CategoryModelOutput uses an implicit operator to convert from Category to CategoryModelOutput.
/// Ex.: CategoryModelOutput createCategoryOutput = category;
/// Where category is an instance of Category.
/// </summary>
public record CategoryModelOutput
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; }
    public DateTime CreatedAt { get; }

    public CategoryModelOutput(Guid id, string name, string description, bool isActive, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public static implicit operator CategoryModelOutput(DomainEntity.Category category)
    {
        return new CategoryModelOutput(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
    }
}