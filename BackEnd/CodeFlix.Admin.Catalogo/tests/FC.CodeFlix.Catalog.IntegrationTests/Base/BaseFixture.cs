using Bogus;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.IntegrationTests.Base;

public class BaseFixture
{
    protected Faker Faker;

    public BaseFixture()
     => Faker = new Faker();

    public CodeFlixCatalogDbContext CreateDbContext(bool preserveData = false)
    {

        var options = new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
            // .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInMemoryDatabase("integration-tests-db")
            .Options;

        var context = new CodeFlixCatalogDbContext(options);

        if (preserveData is false)
            context.Database.EnsureDeleted();
        return context;
    }
}
