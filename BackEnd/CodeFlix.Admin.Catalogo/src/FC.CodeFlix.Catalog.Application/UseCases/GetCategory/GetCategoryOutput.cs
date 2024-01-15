namespace FC.CodeFlix.Catalog.Application.UseCases.GetCategory;

public record GetCategoryOutput(Guid Id, string Name, string Description, bool IsActive, DateTime CreatedAt);
