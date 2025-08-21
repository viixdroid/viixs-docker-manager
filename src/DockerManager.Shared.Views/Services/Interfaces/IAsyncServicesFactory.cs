using Microsoft.JSInterop;

namespace DockerManager.Shared.Views.Services.Interfaces;

public interface IAsyncServicesFactory
{
    Task<ILocalStorageService> GetLocalStorageService(IJSRuntime jsRuntime);
    Task<IThemeService> GetThemeService(IJSRuntime jsRuntime);
}
