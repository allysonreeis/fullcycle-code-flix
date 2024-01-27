using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.GetCategoryUseCase;

public class GetCategoryTestFixture : CategoryUseCasesBaseFixture
{


    public Mock<ICategoryRepository> GetRepositoryMock() => new();

    public Category GetValidCategory()
            => new(GetValidCategoryName(), GetValidCategoryDescription());


}

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture> { }