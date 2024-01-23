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

    public static IEnumerable<object[]> GetInvalidInputsWithExceptions(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int i = 0; i < times; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputShortName(),
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputLongName(),
                        "Name should be at most 255 characters long"
                    });
                    break;
                case 2:
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputDescription(),
                        "Description should be at most 10000 characters long"
                    });
                    break;
                case 3:
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputDescriptionNull(),
                        "Description should not be null"
                    });
                    break;
            }
        }

        return invalidInputList;
    }

}