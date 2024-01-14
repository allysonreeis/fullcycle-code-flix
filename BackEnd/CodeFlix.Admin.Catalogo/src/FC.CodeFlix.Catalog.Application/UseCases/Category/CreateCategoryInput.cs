using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category;

public record CreateCategoryInput : IRequest<CreateCategoryOutput>
{
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; }

    public CreateCategoryInput(string name, string? description = null, bool isActive = true)
    {
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
    }
}