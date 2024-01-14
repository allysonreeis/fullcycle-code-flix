using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CreateCategoryOutput>
{
    Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
}