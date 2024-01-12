using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.CategoryTests;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture() : base()
    {
    }

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

    public string GetInvalidCategoryName()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        while (categoryDescription.Length <= 10_000)
            categoryDescription += Faker.Commerce.ProductDescription();
        return categoryDescription;
    }

    public Category GetValidCategory()
        => new(GetValidCategoryName(), GetValidCategoryDescription());
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{
}