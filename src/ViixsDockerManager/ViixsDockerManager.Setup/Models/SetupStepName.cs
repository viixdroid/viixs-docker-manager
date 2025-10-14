using System.Collections.Immutable;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Setup.Models;

internal record SetupStepName
{
    public static SetupStepName Welcome = new("Welcome");
    public static SetupStepName CreateNewAccount = new("CreateNewAccount");
    public static SetupStepName ConnectToDockLightEnvironment = new("ConnectToDockLightEnvironment");
    public static SetupStepName Finish = new("Finish");

    private static readonly ImmutableArray<SetupStepName> _steps = [
        Welcome,
        CreateNewAccount,
        ConnectToDockLightEnvironment,
        Finish
    ];

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

    public static explicit operator SetupStepName(string stepName)
    {
        stepName = Guard.ValueIsNotNull(stepName, nameof(stepName));
        var knownStep = _steps.FirstOrDefault(s => s.StepName.Equals(stepName, StringComparison.OrdinalIgnoreCase));
        knownStep = Guard.ValueIsNotNull(knownStep, nameof(knownStep));

        return knownStep;
    }

    public static SetupStepName? GetNextStep(SetupStepName currentStep)
    {
        currentStep = Guard.ValueIsNotNull(currentStep, nameof(currentStep));
        var currentIndex = _steps.IndexOf(currentStep);
        if (currentIndex == -1 || currentIndex == _steps.Length - 1)
        {
            return null; // No next step available
        }

        return _steps[currentIndex + 1];
    }

    public static SetupStepName? GetPreviousStep(SetupStepName currentStep)
    {
        currentStep = Guard.ValueIsNotNull(currentStep, nameof(currentStep));
        var currentIndex = _steps.IndexOf(currentStep);
        if (currentIndex <= 0)
        {
            return null; // No previous step available
        }

        return _steps[currentIndex - 1];
    }
}
