using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Exceptions;

namespace ViixsDockerManager.Shared.Database.Contexts;

public abstract class BaseReadDbContext(DbContextOptions options) : DbContext(options)
{
    public sealed override int SaveChanges()
        => throw new CannotWriteInAReadContextException();

    public sealed override int SaveChanges(bool acceptAllChangesOnSuccess)
        => throw new CannotWriteInAReadContextException();

    public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => throw new CannotWriteInAReadContextException();

    public sealed override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
        => throw new CannotWriteInAReadContextException();
}
