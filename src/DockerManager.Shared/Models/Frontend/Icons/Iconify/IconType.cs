using System.ComponentModel;

namespace DockerManager.Shared.Models.Frontend.Icons.Iconify;

public interface IIconType
{
    
}

public record SolarIconTypeV2 : IIconType
{
    private readonly string _type;

    private SolarIconTypeV2(string type)
    {
        _type = type;
    }

    // public static IconTypeV2 Unknown => new IconTypeV2("unknown");
    public static SolarIconTypeV2 Broken => new SolarIconTypeV2("broken");
    public static SolarIconTypeV2 LineDuotone => new SolarIconTypeV2("line-duotone");
    public static SolarIconTypeV2 Linear => new SolarIconTypeV2("linear");
    public static SolarIconTypeV2 Outline => new SolarIconTypeV2("outline");
    public static SolarIconTypeV2 Bold => new SolarIconTypeV2("bold");
    public static SolarIconTypeV2 BoldDuotone => new SolarIconTypeV2("bold-duotone");

    public override string ToString() => _type;

    public static implicit operator string(SolarIconTypeV2 solarIconType) => solarIconType.ToString();
}

public record FluentIconTypeV2 : IIconType
{
    private readonly string _type;

    private FluentIconTypeV2(string type)
    {
        _type = type;
    }

    public static FluentIconTypeV2 Regular => new FluentIconTypeV2("regular");
    public static FluentIconTypeV2 Filled => new FluentIconTypeV2("filled");
    public static FluentIconTypeV2 Light => new FluentIconTypeV2("light");
    public static FluentIconTypeV2 Color => new FluentIconTypeV2("color");

    public override string ToString() => _type;

    public static implicit operator string(FluentIconTypeV2 fluentIconType) => fluentIconType.ToString();
}