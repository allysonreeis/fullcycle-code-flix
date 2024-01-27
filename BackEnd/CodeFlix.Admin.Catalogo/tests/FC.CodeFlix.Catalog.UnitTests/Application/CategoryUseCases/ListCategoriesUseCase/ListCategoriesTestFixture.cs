using FC.CodeFlix.Catalog.Application.UseCases.ListCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.ListCategoriesUseCase;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
{

}


public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Category GetValidCategory()
            => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());


    public List<Category> GetValidCategoriesList(int length = 10)
    {
        var categories = new List<Category>();
        for (var i = 0; i < length; i++)
            categories.Add(GetValidCategory());
        return categories;
    }

    public ListCategoriesInput GetExampleInput()
    {
        var random = new Random();
        return new ListCategoriesInput(
            page: random.Next(1, 10),
            perPage: random.Next(1, 10),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: Faker.Random.Enum<SearchOrder>()
        );
    }
}