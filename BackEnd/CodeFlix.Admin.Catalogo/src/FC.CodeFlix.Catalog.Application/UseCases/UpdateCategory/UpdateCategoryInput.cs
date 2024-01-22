using FC.CodeFlix.Catalog.Application.UseCases.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;

public record UpdateCategoryInput(Guid Id, string Name, string Description, bool IsActive) : IRequest<CategoryModelOutput>;