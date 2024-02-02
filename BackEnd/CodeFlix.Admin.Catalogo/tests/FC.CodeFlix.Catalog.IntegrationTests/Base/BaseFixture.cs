using Bogus;

namespace FC.CodeFlix.Catalog.IntegrationTests.Base;

public class BaseFixture
{
    protected Faker Faker;

    public BaseFixture()
     => Faker = new Faker();

}
