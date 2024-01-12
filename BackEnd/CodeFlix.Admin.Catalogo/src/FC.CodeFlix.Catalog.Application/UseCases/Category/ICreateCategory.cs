namespace FC.CodeFlix.Catalog.Application.UseCases.Category;

public interface ICreateCategory
{
    Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
}