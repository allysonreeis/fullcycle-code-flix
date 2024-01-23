using FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategoryUseCase;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest
{
    private readonly UpdateCategoryTestFixture _fixture;

    public UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void DontValidateWhenEmptyGuid()
    {
        var input = _fixture.GetValidInput(Guid.Empty);

        var validator = new UpdateCategoryInputValidator();

        var result = validator.Validate(input);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().ErrorMessage.Should().Be("Id should not be empty");
    }

    [Fact]
    public void ValidateOk()
    {
        var input = _fixture.GetValidInput();

        var validator = new UpdateCategoryInputValidator();

        var result = validator.Validate(input);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }
}