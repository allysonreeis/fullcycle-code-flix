using System.Runtime.InteropServices;
using FC.CodeFlix.Catalog.Application.UseCases.GetCategory;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.GetCategoryUseCase;

public class GetCategoryInputValidatorTest
{
    [Fact]
    public void ValidationOk()
    {
        var input = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidator();

        var result = validator.Validate(input);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void InvalidWhenEmptyGuidId()
    {
        var invalid = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidator();

        var result = validator.Validate(invalid);

        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorMessage.Should().Be("Id is required");
    }
}