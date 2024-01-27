using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.UpdateCategoryUseCase;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture> { }

public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public Category GetValidCategory()
                => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public UpdateCategoryInput GetValidInput(Guid? id = null)
        => new(id ?? Guid.NewGuid(), GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBoolean());

    public bool GetRandomBoolean()
        => Faker.Random.Bool();

    public UpdateCategoryInput GetInvalidInputLongName()
    {
        var categoryName = "";
        while (categoryName.Length < 255)
            categoryName += Faker.Commerce.Categories(1)[0];
        var input = new UpdateCategoryInput(Guid.NewGuid(), categoryName, GetValidCategoryDescription(), GetRandomBoolean());

        return input;
    }

    public UpdateCategoryInput GetInvalidInputShortName()
    {
        var categoryName = Faker.Commerce.Categories(1)[0];
        var updateCategory = new UpdateCategoryInput(Guid.NewGuid(), categoryName[..2], GetValidCategoryDescription(), GetRandomBoolean());

        return updateCategory;
    }

    public UpdateCategoryInput GetInvalidInputDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        while (categoryDescription.Length <= 10_000)
            categoryDescription += Faker.Commerce.ProductDescription();

        var input = new UpdateCategoryInput(Guid.NewGuid(), GetValidCategoryName(), categoryDescription, GetRandomBoolean());

        return input;
    }

    public UpdateCategoryInput GetInvalidInputDescriptionNull()
        => new(Guid.NewGuid(), GetValidCategoryName(), null!, GetRandomBoolean());
}