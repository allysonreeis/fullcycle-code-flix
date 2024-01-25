using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Common;

namespace FC.CodeFlix.Catalog.Application.UseCases.ListCategory;

public class ListCategoriesOutput : PaginatedListOutput<CategoryModelOutput>
{
    public ListCategoriesOutput(int page, int perPage, int total, IReadOnlyList<CategoryModelOutput> items) : base(page, perPage, total, items)
    {
    }
}