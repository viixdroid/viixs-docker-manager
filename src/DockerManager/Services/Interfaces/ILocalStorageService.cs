namespace DockerManager.Services.Interfaces;

public interface ILocalStorageService
{
    Task<TResult> Get<TResult>(string key);
    Task Set<TResult>(string key, TResult value);
}
