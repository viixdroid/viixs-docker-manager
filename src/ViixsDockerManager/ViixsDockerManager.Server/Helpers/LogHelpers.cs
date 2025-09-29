using Microsoft.Extensions.Configuration;
using Serilog;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Server.Helpers;

internal static class LogHelpers
{
    private const string LogTemplate = "[{Timestamp:HH:mm:ss}] [{Level:u4}] [{SourceContext}] {Message:j}{NewLine}{Exception}";
    public static LoggerConfiguration GetLoggerConfiguration(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, IServiceProvider? serviceProvider = null)
    {
        var logFileLocation = configuration.GetSection("ViixsDockerManager").GetValue<string>("logfilelocation") ?? throw ViixsDockerManagerException.InvalidOperation("No log file locations");
        var config = loggerConfiguration.ReadFrom.Configuration(configuration);
        if (serviceProvider is not null)
        {
            config.ReadFrom.Services(serviceProvider);
        }
        config.Enrich.FromLogContext()
              .WriteTo.Console(outputTemplate: LogTemplate)
              .WriteTo.File(logFileLocation!, rollingInterval: RollingInterval.Day, outputTemplate: LogTemplate)
              .WriteTo.OpenTelemetry(
                  // The endpoint below needs to be accessible from your service.
                  // Aspire automatically sets up environment variables for the OTLP endpoint.
                  // You might use Configuration.GetConnectionString("otlp") or a specific env var.
                  endpoint: configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] ?? "http://localhost:4317",
                  protocol: Serilog.Sinks.OpenTelemetry.OtlpProtocol.Grpc,
                 restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
              );
        return config;
    }
}
