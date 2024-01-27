using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.Common;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.DeleteCategoryUseCase;

public class DeleteCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public Category GetValidCategory()
            => new(GetValidCategoryName(), GetValidCategoryDescription());

}

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture> { }