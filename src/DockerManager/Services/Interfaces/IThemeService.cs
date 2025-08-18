namespace DockerManager.Services.Interfaces;

public interface IThemeService
{
    Task SetThemeAsync(string theme);
    Task SaveThemeAsync(string theme);

    Task<string> GetCurrentThemeAsync();
}
