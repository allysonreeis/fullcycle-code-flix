using System.Formats.Tar;
using FC.CodeFlix.Catalog.Application.UseCases.Common;
using FC.CodeFlix.Catalog.Application.UseCases.ListCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.ListCategoriesUseCase;

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
        var input = new ListCategoriesInput(
            page: 2,
            perPage: 10,
            search: "search",
            sort: "name",
            dir: SearchOrder.Asc
        );

        var outputRepositorySearch = new SearchOutput<Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: (IReadOnlyList<Category>)categoriesList,
                total: 10
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