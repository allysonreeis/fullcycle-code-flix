
using FC.CodeFlix.Catalog.Application.UseCases.Common;
using FC.CodeFlix.Catalog.Domain.Repository;

namespace FC.CodeFlix.Catalog.Application.UseCases.ListCategory;

public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
    {
        var searchOutput = await _categoryRepository.Search(
            new(request.Page, request.PerPage, request.Search, request.Sort, request.Dir),
            cancellationToken
        );

        var listCategoriesOutput = new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items.Select(c => (CategoryModelOutput)c).ToList()
        );

        return listCategoriesOutput;
    }
}