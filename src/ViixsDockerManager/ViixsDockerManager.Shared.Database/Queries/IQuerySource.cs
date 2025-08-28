namespace ViixsDockerManager.Shared.Database.Queries;

public interface IQuerySource<out TQueryResult>
{
    IQueryable<TQueryResult> AsQueryable();
}
