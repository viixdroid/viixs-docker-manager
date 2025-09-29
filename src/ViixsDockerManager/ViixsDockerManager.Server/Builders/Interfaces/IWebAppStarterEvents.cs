using ViixsDockerManager.Server.Builders.EventHandlers.EventArguments;

namespace ViixsDockerManager.Server.Builders.Interfaces;

public interface IWebAppStarterEvents
{
    event EventHandler<InitializingEventArgs>? OnInitializing;
    event EventHandler<InitializedEventArgs>? OnInitialized;

    event EventHandler<ConfiguringServicesEventArgs>? OnConfiguringServices;
    event EventHandler<ServicesConfiguredEventArgs>? OnServicesConfigured;

    event EventHandler<ConfiguringApplicationEventArgs>? OnConfiguringApplication;
    event EventHandler<ApplicationConfiguredEventArgs>? OnApplicationConfigured;

    event EventHandler<StartingApplicationEventArgs>? OnApplicationStarting;
    event EventHandler<ApplicationStartedEventArgs>? OnApplicationStarted;
}
