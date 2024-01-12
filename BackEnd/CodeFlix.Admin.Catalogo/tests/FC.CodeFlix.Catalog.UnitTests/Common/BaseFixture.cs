using Bogus;

namespace FC.CodeFlix.Catalog.UnitTests.Common;

public class BaseFixture
{
    protected Faker Faker { get; }
    protected BaseFixture() => Faker = new Faker("pt_BR");

}