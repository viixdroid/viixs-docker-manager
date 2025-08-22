using DockerManager.Shared.Helpers;
using DockerManager.Shared.Services.Interfaces;
using DockerManager.Shared.Views.Services.Interfaces;
using Microsoft.JSInterop;

namespace DockerManager.Shared.Views.Services;

public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService, IAsyncInitialization
{
    private const string GetItem = "getItem";
    private const string SetItem = "setItem";
    private const string ImportModule = "./js/LocalStorageService.js";
    private const string Import = "import";

    private IJSObjectReference? _localStorageModule;

    public Task<TResult> Get<TResult>(string key)
    {
        Guard.Against.Null(_localStorageModule, nameof(_localStorageModule));
        return _localStorageModule!.InvokeAsync<TResult>(GetItem, key).AsTask();
    }

    public async Task Set<TResult>(string key, TResult value)
    {
        Guard.Against.Null(_localStorageModule, nameof(_localStorageModule));
        await _localStorageModule!.InvokeVoidAsync(SetItem, key, value);
    }

    public async Task InitializeAsync()
    {
        _localStorageModule = await jsRuntime.InvokeAsync<IJSObjectReference>(Import, ImportModule);
    }
}
