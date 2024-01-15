using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.GetCategory;

public record GetCategoryInput(Guid Id) : IRequest<GetCategoryOutput>
{
}