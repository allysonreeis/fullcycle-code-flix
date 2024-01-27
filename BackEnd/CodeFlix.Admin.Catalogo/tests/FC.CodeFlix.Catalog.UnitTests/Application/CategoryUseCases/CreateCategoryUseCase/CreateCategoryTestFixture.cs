using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.CreateCategoryUseCase;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>
{

}


public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{

    public string GetLongCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 255)
            categoryName += Faker.Commerce.Categories(1)[0];
        return categoryName;
    }

    public string GetShortCategoryName()
    {
        var categoryName = Faker.Commerce.Categories(1)[0];

        return categoryName[..2];
    }

    public string GetInvalidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        while (categoryDescription.Length <= 10_000)
            categoryDescription += Faker.Commerce.ProductDescription();
        return categoryDescription;
    }



    public CreateCategoryInput GetValidInput()
        => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
}