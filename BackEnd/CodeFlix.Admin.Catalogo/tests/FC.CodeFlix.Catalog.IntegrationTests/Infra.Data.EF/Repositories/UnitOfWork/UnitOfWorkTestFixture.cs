using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.IntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.UnitOfWork;


[CollectionDefinition(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTestFixstureCollection : ICollectionFixture<UnitOfWorkTestFixture>
{

}

public class UnitOfWorkTestFixture : BaseFixture
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

}
