using FC.CodeFlix.Catalog.Application.UseCases.UpdateCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UpdateCategoryUseCase;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetValidCategoryData(int times = 10)
    {
        UpdateCategoryTestFixture fixture = new UpdateCategoryTestFixture();

        for (int i = 0; i < times; i++)
        {
            var exampleCategory = fixture.GetValidCategory();
            var input = new UpdateCategoryInput(
                exampleCategory.Id,
                fixture.GetValidCategoryName(),
                fixture.GetValidCategoryDescription(),
                !exampleCategory.IsActive
            );
            yield return new object[]
            {
                exampleCategory,
                input
            };
        }
    }
}