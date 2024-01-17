using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.DeleteCategory;

public record DeleteCategoryInput(Guid Id) : IRequest;