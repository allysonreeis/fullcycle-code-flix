using FC.CodeFlix.Catalog.Application.UseCases.Common;
using FluentAssertions;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategoryUseCase;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldUpdateCategory()
    {
        var category = _fixture.GetValidCategory();
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock.Setup(x => x.Get(
            category.Id, It.IsAny<CancellationToken>()
        )).ReturnsAsync(category);

        var input = new UpdateCategoryInput(
            _fixture.GetValidCategoryName(),
            _fixture.GetValidCategoryDescription(),
            !category.IsActive
        );

        var useCase = new UpdateCategoryUseCase(repositoryMock.Object, unitOfWorkMock.Object);

        CategoryModelOutput result = await useCase.Handle(input, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(category.Id);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);
        result.IsActive.Should().Be(input.IsActive);

        repositoryMock.Verify(x => x.Get(
            category.Id, It.IsAny<CancellationToken>()
        ), Times.Once);
        repositoryMock.Verify(x => x.Update(
            category, It.IsAny<CancellationToken>()
        ), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

    }
}