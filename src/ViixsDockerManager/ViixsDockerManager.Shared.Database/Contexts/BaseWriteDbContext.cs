using Microsoft.EntityFrameworkCore;

namespace ViixsDockerManager.Shared.Database.Contexts;

public class BaseWriteDbContext(DbContextOptions options) : DbContext(options), IWriteDbContext
{

}
