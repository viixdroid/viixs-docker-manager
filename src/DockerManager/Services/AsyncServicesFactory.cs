using DockerManager.Services.Interfaces;
using Microsoft.JSInterop;

namespace DockerManager.Services;

public class AsyncServicesFactory //Should keep constructorless
    : IAsyncServicesFactory
{
    private ILocalStorageService? _localStorageService;
    private IThemeService? _themeService;

    public async Task<ILocalStorageService> GetLocalStorageService(IJSRuntime jsRuntime)
    {
        if (_localStorageService is not null)
        {
            return _localStorageService;
        }

        _localStorageService = new LocalStorageService(jsRuntime);

        if (_localStorageService is IAsyncInitialization asyncInitialization)
        {
            await asyncInitialization.InitializeAsync();
        }

        return _localStorageService;
    }

    public async Task<IThemeService> GetThemeService(IJSRuntime jsRuntime)
    {
        if (_themeService is not null)
        {
            return _themeService;
        }

        var localStorageService = await GetLocalStorageService(jsRuntime);
        _themeService = new ThemeService(jsRuntime, localStorageService);

        if (_themeService is IAsyncInitialization asyncInitialization)
        {
            await asyncInitialization.InitializeAsync();
        }

        return _themeService;
    }
}
