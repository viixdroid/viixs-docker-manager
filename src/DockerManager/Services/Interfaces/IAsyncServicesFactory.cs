using Microsoft.JSInterop;

namespace DockerManager.Services.Interfaces;

public interface IAsyncServicesFactory
{
    Task<ILocalStorageService> GetLocalStorageService(IJSRuntime jsRuntime);
    Task<IThemeService> GetThemeService(IJSRuntime jsRuntime);
}
