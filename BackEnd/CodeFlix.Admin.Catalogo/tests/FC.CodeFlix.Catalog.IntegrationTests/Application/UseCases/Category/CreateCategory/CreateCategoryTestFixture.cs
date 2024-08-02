using FC.CodeFlix.Catalog.Application.UseCases.Category;
using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{
}

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public CreateCategoryInput GetInput()
    {
        var category = GetExampleCategory();
        return new CreateCategoryInput(category.Name, category.Description, category.IsActive);
    }
}
