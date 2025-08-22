using DockerManager.Shared.Helpers;
using DockerManager.Shared.Services.Interfaces;
using DockerManager.Shared.Views.Services.Interfaces;
using Microsoft.JSInterop;

namespace DockerManager.Shared.Views.Services;

public class ThemeService(IJSRuntime jsRuntime, ILocalStorageService localStorageService)
    : IThemeService, IAsyncInitialization
{
    private const string GetCurrentTheme = "getCurrentTheme";
    private const string SetTheme = "setTheme";
    private const string ImportModule = "./js/ThemeService.js";
    private const string Import = "import";

    private IJSObjectReference? _themeModule;

    public async Task SetThemeAsync(string theme)
    {
        Guard.Against.Null(_themeModule, nameof(_themeModule));
        await _themeModule!.InvokeVoidAsync(SetTheme, theme);
    }

    public Task<string> GetCurrentThemeAsync()
    {
        Guard.Against.Null(_themeModule, nameof(_themeModule));
        return _themeModule!.InvokeAsync<string>(GetCurrentTheme).AsTask();
    }

    public async Task SaveThemeAsync(string theme)
    {
        Guard.Against.Null(_themeModule, nameof(_themeModule));
        await localStorageService.Set("theme", theme);
    }


    public async Task InitializeAsync() =>
        _themeModule = await jsRuntime.InvokeAsync<IJSObjectReference>(Import, ImportModule);
}
