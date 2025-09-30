using ViixsDockerManager.Server;

var webApp = WebAppStarter.Instance    
    .ConfigureEventHandlers()
    .CreateWebApplicationBuilder(args, builder =>
{
    builder.Configuration.AddEnvironmentVariables();
    builder.AddServiceDefaults();
})
    .ConfigureWebAppBuilder()
    .BuildWebApp()
    .RunMigrations()
    .ConfigureApplication();

await webApp.RunApp(default);
