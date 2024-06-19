using FC.CodeFlix.Catalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.CodeFlix.Catalog.Infra.Data.EF;
public class UnitOfWork : IUnitOfWork
{
    private readonly CodeFlixCatalogDbContext _context;

    public UnitOfWork(CodeFlixCatalogDbContext context)
    {
        _context = context;
    }

    public Task Commit(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
