using FC.CodeFlix.Catalog.Application.UseCases.GetCategory;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCase = FC.CodeFlix.Catalog.Application.UseCases.GetCategory;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetCategory()
    {
        var exampleCategory = _fixture.GetExampleCategory();
        var context = _fixture.CreateDbContext();
        context.Categories.Add(exampleCategory);
        context.SaveChanges();
        var repository = new CategoryRepository(context);

        var useCase = new UseCase.GetCategory(repository);
        var input = new GetCategoryInput(exampleCategory.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }
}
