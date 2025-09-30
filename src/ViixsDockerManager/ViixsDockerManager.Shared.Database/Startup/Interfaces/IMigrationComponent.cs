using Microsoft.EntityFrameworkCore;
using ViixsDockerManager.Shared.Database.Contexts;
using ViixsDockerManager.Shared.Startup.Interfaces;

namespace ViixsDockerManager.Shared.Database.Startup.Interfaces;

public interface IMigrationComponent : IApplicationBuilderComponent 
{
    Type GetMigrationDbContextType();
}
