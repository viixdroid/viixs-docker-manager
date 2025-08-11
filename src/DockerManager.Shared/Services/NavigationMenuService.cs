using DockerManager.Shared.Models.Frontend;
using DockerManager.Shared.Models.Frontend.Icons.Iconify;

namespace DockerManager.Shared.Services;

public class NavigationMenuService : INavigationMenuService
{
    public IReadOnlyList<NavigationMenuItem> GetNavigationMenuItems()
    {
        return
        [
            new NavigationMenuItem("Containers", "/containers", Icon.MaterialDesign("server")),
            new NavigationMenuItem("Settings", "/settings", Icon.MaterialDesign("settings"))
        ];
    }
}