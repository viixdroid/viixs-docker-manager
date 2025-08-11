using DockerManager.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace DockerManager.Components.Layout;

public partial class SideBarLayout(INavigationMenuService navigationMenuService) : LayoutComponentBase
{
    private bool _isCollapsed;

    private void ToggleSidebar() => _isCollapsed = !_isCollapsed;
}