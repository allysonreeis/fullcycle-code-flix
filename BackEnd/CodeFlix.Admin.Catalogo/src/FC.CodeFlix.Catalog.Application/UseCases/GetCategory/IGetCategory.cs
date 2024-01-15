using FC.CodeFlix.Catalog.Application.UseCases.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.GetCategory;

public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOutput>
{
}