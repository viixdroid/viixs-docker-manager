using System.ComponentModel;

namespace DockerManager.Shared.Models.Frontend.Icons.Iconify;

public record IconFamily
{
    private readonly string _family;

    private IconFamily(string family)
    {
        _family = family;
    }

    public static IconFamily MaterialSymbols => new IconFamily("material-symbols");
    public static IconFamily MaterialSymbolsLight => new IconFamily("material-symbols-light");
    public static IconFamily Ic => new IconFamily("ic");
    public static IconFamily Mdi => new IconFamily("mdi");
    public static IconFamily MdiLight => new IconFamily("mdi-light");
    public static IconFamily MaterialDesignIcons => new IconFamily("material-design-icons");
    public static IconFamily LineMd => new IconFamily("line-md");
    public static IconFamily Solar => new IconFamily("solar");
    public static IconFamily Fluent => new IconFamily("fluent");
    public static IconFamily Fa7Regular => new IconFamily("fa7-regular");
    public static IconFamily FluentColor => new IconFamily("fluent-color");
    public static IconFamily MaterialIconTheme => new IconFamily("material-icon-theme");
    public static IconFamily SimpleIcons => new IconFamily("simple-icons");

    public override string ToString() => _family;

    public static implicit operator string(IconFamily iconFamily) => iconFamily.ToString();
}