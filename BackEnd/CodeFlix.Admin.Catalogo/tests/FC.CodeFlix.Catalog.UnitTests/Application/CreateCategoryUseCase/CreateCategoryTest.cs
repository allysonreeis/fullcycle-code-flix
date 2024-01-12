using FC.CodeFlix.Catalog.Domain.Entity;
using Moq;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategoryUseCase;

public class CreateCategoryTest
{
    [Fact]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = new CreateCategoryInput("Category Name", "Category Description", true);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository => repository.Create(It.IsAny<Category>(), It.IsAny<CancellationToken>()), times: Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), times: Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Id.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        (output.CreatedAt != null && output.CreatedAt != default(DateTime)).Should().BeTrue();
    }
}