using DockerManager.Shared.Models.Frontend;

namespace DockerManager.Shared.Services;

public interface INavigationMenuService
{
    IReadOnlyList<NavigationMenuItem> GetNavigationMenuItems();
}