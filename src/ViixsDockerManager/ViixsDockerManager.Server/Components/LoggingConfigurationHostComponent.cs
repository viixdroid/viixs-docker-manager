using Serilog;
using ViixsDockerManager.Server.Helpers;
using ViixsDockerManager.Shared.AspNet.Startup;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Server.Components;

internal class LoggingConfigurationHostComponent() : HostComponent
{
    protected override void ConfigureHost(IHostBuilder hostBuilder)
        => hostBuilder.UseSerilog((context, services, configuration) => configuration.GetLoggerConfiguration(context.Configuration, services));
}
