namespace ViixsDockerManager.Setup.Models.Dtos;

internal record IsSetupFinished(bool IsFinished)
{
    public static implicit operator IsSetupFinished(bool isSetupFinished)
    {
        return new IsSetupFinished(isSetupFinished);
    }
}
