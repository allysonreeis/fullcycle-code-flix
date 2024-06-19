using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using uow = FC.CodeFlix.Catalog.Infra.Data.EF;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture unitOfWorkTestFixture)
    {
        _fixture = unitOfWorkTestFixture;
    }

    [Fact]
    public async Task Commit()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleCategoryList = _fixture.GetExampleCategoryList();
        await dbContext.Categories.AddRangeAsync(exampleCategoryList);
        var unitOfWork = new uow.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var savedCategories = assertDbContext.Categories
            .AsNoTracking().ToList();

        savedCategories.Should().HaveCount(exampleCategoryList.Count);
    }

    [Fact]
    public async Task Rollback()
    {
        var dbContext = _fixture.CreateDbContext();
        var unitOfWork = new uow.UnitOfWork(dbContext);
        var task = async () => await unitOfWork.Rollback(CancellationToken.None);
        await task.Should().NotThrowAsync();
    }
}
