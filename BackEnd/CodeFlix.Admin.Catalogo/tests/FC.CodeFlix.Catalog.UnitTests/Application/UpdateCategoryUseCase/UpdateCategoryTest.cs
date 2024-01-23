using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Application.UseCases.Common;
using FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
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

    [Theory]
    [MemberData(nameof(UpdateCategoryTestDataGenerator.GetValidCategoryData), parameters: 10, MemberType = typeof(UpdateCategoryTestDataGenerator))]
    public async Task ShouldUpdateCategory(Category category, UpdateCategoryInput input)
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock.Setup(x => x.Get(
            category.Id, It.IsAny<CancellationToken>()
        )).ReturnsAsync(category);

        var useCase = new UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

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

    [Fact]
    public void ShouldThrowCategoryNotFound()
    {
        var repositoryMock = _fixture.GetCategoryRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var input = _fixture.GetValidInput(Guid.NewGuid());

        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()
        )).ThrowsAsync(new NotFoundException($"Category '{input.Id}' not found"));

        var useCase = new UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        Func<Task> act = async () => await useCase.Handle(
            input, CancellationToken.None
        );

        act.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(), It.IsAny<CancellationToken>()
        ), Times.Once);
        repositoryMock.Verify(x => x.Update(
            It.IsAny<Category>(), It.IsAny<CancellationToken>()
        ), Times.Never);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }


}