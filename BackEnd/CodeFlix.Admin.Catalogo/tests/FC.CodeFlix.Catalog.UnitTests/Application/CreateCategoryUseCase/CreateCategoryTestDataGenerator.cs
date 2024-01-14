using FC.CodeFlix.Catalog.Application.UseCases.Category;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategoryUseCase;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInput()
    {
        var fixture = new CreateCategoryTestFixture();

        yield return new object[] { new CreateCategoryInput("", "", true), $"{nameof(CreateCategoryInput.Name)} should not be empty or null" };
        yield return new object[] { new CreateCategoryInput(fixture.GetShortCategoryName(), "", true), $"{nameof(CreateCategoryInput.Name)} should be greater or equal to 3 characters long" };
        yield return new object[] { new CreateCategoryInput(fixture.GetLongCategoryName(), "", true), $"{nameof(CreateCategoryInput.Name)} should be less or equal to 255 characters long" };
        yield return new object[] { new CreateCategoryInput(fixture.GetValidCategoryName(), "", true), $"{nameof(CreateCategoryInput.Description)} should not be empty or null" };
        yield return new object[] { new CreateCategoryInput(fixture.GetValidCategoryName(), fixture.GetInvalidCategoryDescription(), true), $"{nameof(CreateCategoryInput.Description)} should be less or equal to 10000 characters long" };
        yield return new object[] { new CreateCategoryInput(fixture.GetValidCategoryName(), null, true), $"{nameof(CreateCategoryInput.Description)} should not be null" };
    }
}