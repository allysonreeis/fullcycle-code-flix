using FC.CodeFlix.Catalog.Application.UseCases.ListCategory;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CategoryUseCases.ListCategoriesUseCase;

public class ListCategoriesTestDataGenerator
{
    public static IEnumerable<object[]> GetInputsWithoutAllParameter(int times)
    {
        var fixture = new ListCategoriesTestFixture();
        var inputExample = fixture.GetExampleInput();

        for (int i = 0; i < times; i++)
        {
            yield return new object[] { new ListCategoriesInput() };
            yield return new object[] { new ListCategoriesInput(page: inputExample.Page) };
            yield return new object[] { new ListCategoriesInput(page: inputExample.Page, perPage: inputExample.PerPage) };
            yield return new object[] { new ListCategoriesInput(page: inputExample.Page, perPage: inputExample.PerPage, search: inputExample.Search) };
            yield return new object[] { new ListCategoriesInput(page: inputExample.Page, perPage: inputExample.PerPage, search: inputExample.Search, sort: inputExample.Sort) };
            yield return new object[] { inputExample };
        }
    }
}