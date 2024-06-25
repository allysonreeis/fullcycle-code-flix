using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.IntegrationTests.Base;
using DM = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;
public class CategoryUseCasesBaseFixture : BaseFixture
{
    public DM.Category GetExampleCategory()
        => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public List<DM.Category> GetExampleCategoryList(int Length = 10)
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

    public List<DM.Category> GetExampleCategoryListWithNames(List<string> names)
        => names.Select(name => new DM.Category(name, GetValidCategoryDescription(), GetRandomBoolean())).ToList();

    public List<DM.Category> CloneCategoryListOrdered(List<DM.Category> categories, string orderBy, SearchOrder searchOrder)
    {
        var categoriesClone = new List<DM.Category>(categories);
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
