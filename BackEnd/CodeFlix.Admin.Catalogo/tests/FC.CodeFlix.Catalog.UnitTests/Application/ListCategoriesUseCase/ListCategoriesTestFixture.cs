using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategoriesUseCase;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
{

}


public class ListCategoriesTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Category GetValidCategory()
            => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];
        return categoryDescription;
    }

    public bool GetRandomBoolean() => Faker.Random.Bool();

    public List<Category> GetValidCategoriesList(int length = 10)
    {
        var categories = new List<Category>();
        for (var i = 0; i < length; i++)
            categories.Add(GetValidCategory());
        return categories;
    }
}