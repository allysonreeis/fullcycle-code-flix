using Bogus;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Validation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new();

    [Fact]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNull(value, "Value");
        action.Should().NotThrow();
    }

    [Fact]
    public void NotNullThrowWhenNull()
    {
        string value = null!;
        Action action = () => DomainValidation.NotNull(value, "Value");
        action.Should().Throw<EntityValidationException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        var value = "";
        Action action = () => DomainValidation.NotNullOrEmpty(value, "Value");
        action.Should().Throw<EntityValidationException>().WithMessage("Value Should not be empty or null");
    }

    [Fact]
    public void NotNullOrEmptyOk()
    {
        var value = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNullOrEmpty(value, "Value");
        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetValuesSmallerThanTheMin), parameters: 6)]
    public void MinLengthThrowWhenLess(string target, int minLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MinLength(target, minLength, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be at least {minLength} characters long");
    }

    public static IEnumerable<object[]> GetValuesSmallerThanTheMin(int numberOfTests = 10)
    {
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length + faker.Random.Int(1, 10);
            yield return new object[] { example, minLength };
        }
    }

    [Theory]
    [MemberData(nameof(GetValuesGreaterThanTheMin), parameters: 6)]
    public void MinLengthOk(string target, int minLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MinLength(target, minLength, fieldName);
        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesGreaterThanTheMin(int numberOfTests)
    {
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var minLength = example.Length - faker.Random.Int(1, 10);
            yield return new object[] { example, minLength };
        }
    }

    [Theory]
    [MemberData(nameof(GetValuesGreaterThanTheMax), parameters: 6)]
    public void MaxLengthThrowWhenGreater(string target, int maxLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);
        action.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should be less or equal to {maxLength} characters long");
    }

    public static IEnumerable<object[]> GetValuesGreaterThanTheMax(int numberOfTests)
    {
        var faker = new Faker();
        yield return new object[] { "123456", 5 };
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length - faker.Random.Int(1, 10);
            yield return new object[] { example, maxLength };
        }
    }

    [Theory]
    [MemberData(nameof(GetValuesLessThanTheMax), parameters: 6)]
    public void MaxLengthOk(string target, int maxLength)
    {
        var fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);
        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesLessThanTheMax(int numberOfTests)
    {
        var faker = new Faker();
        yield return new object[] { "123456", 6 };
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length + faker.Random.Int(0, 5);
            yield return new object[] { example, maxLength };
        }
    }
}