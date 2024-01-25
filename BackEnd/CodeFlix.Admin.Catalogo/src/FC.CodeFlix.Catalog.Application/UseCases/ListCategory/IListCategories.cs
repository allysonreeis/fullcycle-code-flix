using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.ListCategory;

public interface IListCategories : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
{

}