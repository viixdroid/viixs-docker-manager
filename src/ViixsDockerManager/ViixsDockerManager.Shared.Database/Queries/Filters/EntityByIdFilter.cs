using System.Linq.Expressions;
using ViixsDockerManager.Shared.Database.Entities;

namespace ViixsDockerManager.Shared.Database.Queries.Filters;

public class EntityByIdFilter<TEntity>(int id) : BaseQueryFilter<TEntity>
    where TEntity : class, IEntity
{
    protected override Expression<Func<TEntity, bool>> GetPredicateExpression()
    {
        return entity => entity.Id == id;
    }
}
