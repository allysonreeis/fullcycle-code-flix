using FC.CodeFlix.Catalog.Application.UseCases.GetCategory;
using FluentAssertions;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategoryUseCase;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCategoryAsync()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var category = _fixture.GetValidCategory();

        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(category);

        var useCase = new GetCategory(repositoryMock.Object);
        var input = new GetCategoryInput(category.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(category.Name);
        output.Description.Should().Be(category.Description);
        output.IsActive.Should().Be(category.IsActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }
}