using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.DeleteCategory;
using FluentAssertions;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.DeleteCategoryUseCase;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest
{
    private readonly DeleteCategoryTestFixture _fixture;

    public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void DeleteCategoryOk()
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var category = _fixture.GetValidCategory();
        var deleteCategoryInput = new DeleteCategoryInput(category.Id);
        var useCase = new DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

        repositoryMock.Setup(r => r.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        await useCase.Handle(deleteCategoryInput, CancellationToken.None);

        repositoryMock.Verify(r => r.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(r => r.Delete(category, CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.Commit(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async void ThrowWhenCategoryNotFound()
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var categoryId = Guid.NewGuid();
        var deleteCategoryInput = new DeleteCategoryInput(categoryId);
        var useCase = new DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

        repositoryMock.Setup(r => r.Get(categoryId, It.IsAny<CancellationToken>())).ThrowsAsync(new NotFoundException($"Category '{categoryId}' not found."));

        var task = async () => await useCase.Handle(deleteCategoryInput, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(r => r.Get(categoryId, It.IsAny<CancellationToken>()), Times.Once);

    }
}