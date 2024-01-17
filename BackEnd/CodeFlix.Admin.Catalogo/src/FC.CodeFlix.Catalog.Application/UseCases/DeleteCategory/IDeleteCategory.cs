using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.DeleteCategory;

public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
}