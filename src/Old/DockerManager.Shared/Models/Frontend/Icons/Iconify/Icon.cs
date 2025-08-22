namespace DockerManager.Shared.Models.Frontend.Icons.Iconify;

public class Icon(IconFamily iconFamily, string name, IIconType? iconType) : IIcon
{
    public string Name => name;
    public IIconType? IconType => iconType;
    public IconFamily IconFamily => iconFamily;

    public string IconString
    {
        get
        {
            //TODO: Guard class
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new NotSupportedException("Icon name cannot be null or empty.");
            }

            if (IconType is null)
            {
                return $"icon-[{IconFamily}--{Name}]";
            }

            return $"icon-[{IconFamily}--{Name}-{IconType}]";
        }
    }

    public static Icon SolarLineDuotone(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.LineDuotone);
    public static Icon SolarBold(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.Bold);
    public static Icon SolarBroken(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.Broken);
    public static Icon SolarLinear(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.Linear);
    public static Icon SolarOutline(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.Outline);
    public static Icon SolarBoldDuotone(string name) => new Icon(IconFamily.Solar, name, SolarIconTypeV2.BoldDuotone);

    public static Icon MaterialDesignLight(string name) => new Icon(IconFamily.MdiLight, name, null);
    public static Icon MaterialDesign(string name) => new Icon(IconFamily.Mdi, name, null);

    public static Icon FluentRegular(string name) => new Icon(IconFamily.Fluent, name, FluentIconTypeV2.Regular);
    public static Icon FluentFilled(string name) => new Icon(IconFamily.Fluent, name, FluentIconTypeV2.Filled);
    public static Icon FluentLight(string name) => new Icon(IconFamily.Fluent, name, FluentIconTypeV2.Light);
    public static Icon FluentColor(string name) => new Icon(IconFamily.FluentColor, name, FluentIconTypeV2.Color);
}