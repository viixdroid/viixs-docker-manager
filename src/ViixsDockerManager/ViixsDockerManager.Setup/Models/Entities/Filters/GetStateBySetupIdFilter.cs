using System.Linq.Expressions;
using ViixsDockerManager.Shared.Database.Queries.Filters;

namespace ViixsDockerManager.Setup.Models.Entities.Filters;

internal sealed class GetStateBySetupIdFilter(Guid? setupId) : IDatabaseQueryFilter<SetupState>
{
    public Expression<Func<SetupState, bool>> GetPredicate()
    {
        if (setupId == null)
        {
            return setupState => false;
        }

        return setupState => setupState.SetupId == setupId;
    }
}
