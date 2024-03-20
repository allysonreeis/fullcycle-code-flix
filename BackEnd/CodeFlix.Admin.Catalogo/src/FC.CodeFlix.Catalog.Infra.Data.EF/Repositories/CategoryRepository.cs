using FC.CodeFlix.Catalog.Application.Exceptions;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Repository;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CodeFlixCatalogDbContext _context;
    private DbSet<Category> _categories => _context.Set<Category>();

    public CategoryRepository(CodeFlixCatalogDbContext context)
    {
        _context = context;
    }

    public async Task Insert(Category aggregate, CancellationToken cancellationToken)
    {
        await _categories.AddAsync(aggregate, cancellationToken);
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken) => Task.FromResult(_categories.Remove(aggregate));

    public async Task<Category> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categories.FindAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(category, $"Category '{id}' not found");
        return category!;
    }

    public async Task Update(Category aggregate, CancellationToken cancellationToken)
    {
        await Task.FromResult(_categories.Update(aggregate));
    }

    public async Task<SearchOutput<Category>> Search(SearchInput searchInput, CancellationToken cancellationToken)
    {
        var toSkip = searchInput.PerPage * (searchInput.Page - 1);
        var total = await _categories.CountAsync(cancellationToken);
        var items = await _categories.AsNoTracking()
            .Skip(toSkip)
            .Take(searchInput.PerPage)
            .ToListAsync(cancellationToken);
        return new SearchOutput<Category>(searchInput.Page, searchInput.PerPage, total, items);
    }


}