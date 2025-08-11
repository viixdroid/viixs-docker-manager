namespace DockerManager.Shared.Models.Frontend.Icons.Iconify;

public interface IIcon
{
    public string Name { get; }
    public IIconType? IconType { get; }
    public IconFamily IconFamily { get; }
    
    public string IconString { get; }
}