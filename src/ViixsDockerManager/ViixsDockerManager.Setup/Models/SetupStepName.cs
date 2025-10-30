using System.Collections.Immutable;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Setup.Models;

public record SetupStepName
{
    public static readonly SetupStepName Welcome = new SetupStepName("Welcome", 0, true, false);
    public static readonly SetupStepName CreateNewAccount = new SetupStepName("CreateNewAccount", 1, false, false);
    public static readonly SetupStepName ConnectToDockLightEnvironment = new SetupStepName("ConnectToDockLightEnvironment", 2, false, false);
    public static readonly SetupStepName Finish = new SetupStepName("Finish", 3, false, true);

    private static readonly ImmutableArray<SetupStepName> _steps = [
        Welcome,
        CreateNewAccount,
        ConnectToDockLightEnvironment,
        Finish
    ];

    private SetupStepName(string stepName, int order, bool isFirstStep, bool isLastStep)
    {
        StepName = stepName;
        Order = order;
        IsFirstStep = isFirstStep;
        IsLastStep = isLastStep;
    }

    public string StepName { get; }
    public int Order { get; }
    public bool IsFirstStep { get; }
    public bool IsLastStep { get; }

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
