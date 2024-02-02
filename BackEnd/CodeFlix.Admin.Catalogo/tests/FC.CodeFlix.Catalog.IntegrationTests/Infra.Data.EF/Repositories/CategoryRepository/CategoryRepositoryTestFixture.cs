using FC.CodeFlix.Catalog.Domain.Entity;
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

    public CodeFlixCatalogDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new CodeFlixCatalogDbContext(options);
    }
}