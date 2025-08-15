using System.Reflection;
using DockerManager.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Layout;

public partial class SideBarLayout(INavigationMenuService navigationMenuService) : LayoutComponentBase
{
    private bool _isCollapsed;

    private void ToggleSidebar() => _isCollapsed = !_isCollapsed;

    //TODO: Move to own Component
    private static string GetVersionNumber(int amountToSubstring = 14)
    {
        return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion[..amountToSubstring] ?? "Unknown Version";
    }
}
