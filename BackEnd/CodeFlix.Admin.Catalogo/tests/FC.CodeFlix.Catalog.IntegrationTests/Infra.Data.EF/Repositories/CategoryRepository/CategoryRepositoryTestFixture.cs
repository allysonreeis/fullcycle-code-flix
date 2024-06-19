using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture>
{
}

public class CategoryRepositoryTestFixture : BaseFixture
{

    public Category GetExampleCategory()
        => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public List<Category> GetExampleCategoryList(int Length = 10)
        => Enumerable.Range(1, Length).Select(_ => GetExampleCategory()).ToList();

    public bool GetRandomBoolean()
        => Faker.Random.Bool();

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

    public List<Category> GetExampleCategoryListWithNames(List<string> names)
        => names.Select(name => new Category(name, GetValidCategoryDescription(), GetRandomBoolean())).ToList();

    public List<Category> CloneCategoryListOrdered(List<Category> categories, string orderBy, SearchOrder searchOrder)
    {
        var categoriesClone = new List<Category>(categories);
        categoriesClone = (orderBy.ToLower(), searchOrder) switch
        {
            ("name", SearchOrder.Asc) => categoriesClone.OrderBy(c => c.Name).ToList(),
            ("name", SearchOrder.Desc) => categoriesClone.OrderByDescending(c => c.Name).ToList(),
            ("id", SearchOrder.Asc) => categoriesClone.OrderBy(c => c.Id).ToList(),
            ("id", SearchOrder.Desc) => categoriesClone.OrderByDescending(c => c.Id).ToList(),
            ("createdat", SearchOrder.Asc) => categoriesClone.OrderBy(c => c.CreatedAt).ToList(),
            ("createdat", SearchOrder.Desc) => categoriesClone.OrderByDescending(c => c.CreatedAt).ToList(),
            _ => categoriesClone.OrderBy(c => c.Name).ToList()
        };

        return categoriesClone;
    }
}