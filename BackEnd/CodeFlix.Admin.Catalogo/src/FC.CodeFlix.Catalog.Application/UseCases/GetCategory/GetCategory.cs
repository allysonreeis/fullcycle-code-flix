using FC.CodeFlix.Catalog.Domain.Repository;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.GetCategory;

public class GetCategory : IRequestHandler<GetCategoryInput, GetCategoryOutput>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryOutput> Handle(GetCategoryInput input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(input.Id, cancellationToken);
        return new GetCategoryOutput(category.Id, category.Name, category.Description, category.IsActive, category.CreatedAt);
    }
}