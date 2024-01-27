using System.Formats.Tar;
using FC.CodeFlix.Catalog.Application.UseCases.Common;
using FC.CodeFlix.Catalog.Application.UseCases.ListCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.ListCategoriesUseCase;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest
{
    private readonly ListCategoriesTestFixture _fixture;

    public ListCategoriesTest(ListCategoriesTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task List()
    {
        var categoriesList = _fixture.GetValidCategoriesList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();

        var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: categoriesList,
                total: new Random().Next(50, 200)
            );

        repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategories(repositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory.Description);
                outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
            }
        );

        repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ), It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task ListOkWhenEmpty()
    {
        var categoriesList = _fixture.GetValidCategoriesList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();

        var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: new List<Category>().AsReadOnly(),
                total: 0
            );

        repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategories(repositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);


        repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ), It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }

    [Theory]
    [MemberData(
        nameof(ListCategoriesTestDataGenerator.GetInputsWithoutAllParameter),
        parameters: 15,
        MemberType = typeof(ListCategoriesTestDataGenerator)
    )]
    public async Task ListInputWithoutAllParameters(ListCategoriesInput input)
    {
        var categoriesList = _fixture.GetValidCategoriesList();
        var repositoryMock = _fixture.GetRepositoryMock();


        var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: categoriesList,
                total: new Random().Next(50, 200)
            );

        repositoryMock.Setup(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(outputRepositorySearch);

        var useCase = new ListCategories(repositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items
                    .FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(repositoryCategory!.Name);
                outputItem.Description.Should().Be(repositoryCategory.Description);
                outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
            }
        );

        repositoryMock.Verify(x => x.Search(
                It.Is<SearchInput>(
                    searchInput => searchInput.Page == input.Page &&
                                   searchInput.PerPage == input.PerPage &&
                                   searchInput.Search == input.Search &&
                                   searchInput.OrderBy == input.Sort &&
                                   searchInput.Order == input.Dir
                ), It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}