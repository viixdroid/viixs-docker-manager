using DockerManager.Shared.Models.Frontend.Icons.Iconify;

namespace DockerManager.Shared.Models.Frontend;

public record NavigationMenuItem(string Name, string Path, IIcon Icon);