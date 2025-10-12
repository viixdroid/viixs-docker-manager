using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Setup.Models;

internal record SetupStepName
{
    public static SetupStepName Welcome = new("Welcome");
    public static SetupStepName CreateNewAccount = new("CreateNewAccount");
    public static SetupStepName ConnectToDockLightEnvironment = new("ConnectToDockLightEnvironment");
    public static SetupStepName Finish = new("Finish");

    private SetupStepName(string stepName)
    {
        StepName = stepName;
    }

    public string StepName { get; }

    public static implicit operator string(SetupStepName setupStepName)
    {
        setupStepName = Guard.ValueIsNotNull(setupStepName, nameof(setupStepName));
        return setupStepName.StepName;
    }
}
