using System.Runtime.InteropServices;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.CategoryTests;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{
    private readonly CategoryTestFixture _fixture;

    public CategoryTest(CategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOftests = 6)
    {
        var fixture = new CategoryTestFixture();
        for (var i = 0; i < numberOftests; i++)
        {
            var isOdd = i % 2 != 0;
            var name = fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)];
            yield return new object[] { name };
        }
    }

    [Fact]
    public void Instantiate()
    {
        var validCategory = _fixture.GetValidCategory();

        var category = new Category(validCategory.Name, validCategory.Description);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBe(default(Guid));
        category.CreatedAt.Should().NotBeSameDateAs(default);
        category.IsActive.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithActive(bool isActive)
    {
        var validCategory = _fixture.GetValidCategory();

        var category = new Category(validCategory.Name, validCategory.Description, isActive);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBe(default(Guid));
        category.CreatedAt.Should().NotBeSameDateAs(default);
        category.IsActive.Should().Be(isActive);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ShouldThrowErrorWhenInstantiateWithInvalidName(string? name)
    {
        // var action = () => new Category(name, "Description description");
        Action action = () => new Category(name, "Description description");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact]
    public void ShouldThrowErrorWhenDescriptionIsNull()
    {
        var action = () => new Category("Category name", null!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }

    [Theory]
    [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 6)]
    public void ShouldThrowErrorWhenInstantiateWithNameLessThan3Characters(string name)
    {
        var action = () => new Category(name, "Description description");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact]
    public void ShouldThrowErrorWhenInstantiateWithNameGreaterThan255Characters()
    {
        var name = new string('a', 256);
        var action = () => new Category(name, "Description description");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal to 255 characters long");
    }

    [Fact]
    public void ShouldThrowErrorWhenInstantiateWithDescriptionGreaterThan10_000Characters()
    {
        var description = new string('a', 10_001);
        var action = () => new Category("Category Name", description);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal to 10000 characters long");
    }

    [Fact]
    public void Active()
    {
        var validCategory = _fixture.GetValidCategory();

        var category = new Category(validCategory.Name, validCategory.Description, false);
        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactive()
    {
        var validCategory = _fixture.GetValidCategory();

        var category = new Category(validCategory.Name, validCategory.Description);
        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Update()
    {
        var category = _fixture.GetValidCategory();
        var categoryWithNewValues = _fixture.GetValidCategory();

        category.Update(categoryWithNewValues.Name, categoryWithNewValues.Description);

        category.Name.Should().Be(categoryWithNewValues.Name);
        category.Description.Should().Be(categoryWithNewValues.Description);
    }

    [Fact]
    public void UpdateOnlyName()
    {
        var category = _fixture.GetValidCategory();
        var currentDescription = category.Description;
        var validData = new
        {
            Name = "Category name updated",
            Description = "Description description updated"
        };

        category.Update(validData.Name);

        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(currentDescription);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ShouldThrowErrorWhenUpdateWithInvalidName(string? name)
    {
        // var action = () => new Category(name, "Description description");
        var category = _fixture.GetValidCategory();
        Action action = () => category.Update(name);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ab")]
    public void ShouldThrowErrorWhenUpdateWithNameLessThan3Characters(string name)
    {
        var category = _fixture.GetValidCategory();
        var action = () => category.Update(name);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact]
    public void ShouldThrowErrorWhenUpdateWithNameGreaterThan255Characters()
    {
        var category = _fixture.GetValidCategory();
        var name = new string('a', 256);
        var action = () => category.Update(name);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal to 255 characters long");
    }


    [Fact]
    public void ShouldThrowErrorWhenUpdateteWithDescriptionGreaterThan10_000Characters()
    {
        var category = _fixture.GetValidCategory();
        var description = _fixture.GetInvalidCategoryName();
        var action = () => category.Update(category.Name, description);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal to 10000 characters long");
    }
}