using System.Reflection;
using DockerManager.Shared.Services;
using DockerManager.Shared.Views.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DockerManager.Shared.Views.Layouts;

public partial class SideBarLayout(
    INavigationMenuService navigationMenuService,
    IAsyncServicesFactory servicesFactory,
    IJSRuntime jsRuntime) : LayoutComponentBase
{
    private bool _isCollapsed;

    private void ToggleSidebar() => _isCollapsed = !_isCollapsed;

    //TODO: Move to own Component
    private static string GetVersionNumber(int amountToSubstring = 14)
    {
        return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion[..amountToSubstring] ?? "Unknown Version";
    }

    protected override async Task OnInitializedAsync()
    {
        var themeService = await GetThemeService();
        CurrentTheme = await themeService.GetCurrentThemeAsync();
    }

    private Task<IThemeService> GetThemeService() => servicesFactory.GetThemeService(jsRuntime);
    private string? CurrentTheme { get; set; }

    private async Task SetChosenThemeAsync(string? themeName)
    {
        if (string.IsNullOrEmpty(themeName) || string.IsNullOrEmpty(CurrentTheme))
        {
            themeName = "nord";
        }

        if (CurrentTheme!.Equals(themeName, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        CurrentTheme = themeName;

        Console.WriteLine(CurrentTheme);

        var themeService = await GetThemeService();
        await themeService.SaveThemeAsync(themeName);
    }
}
