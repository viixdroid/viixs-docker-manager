using DockerManager.Auth.Extensions;
using DockerManager.Auth.Views.Extensions;
using DockerManager.Components;
using DockerManager.DockerControl.Extensions;
using DockerManager.DockerController.Views.Extensions;
using DockerManager.Extensions;
using DockerManager.Shared.Extensions;
using DockerManager.Shared.Views.Extensions;
using static DockerManager.Shared.Constants.ApplicationConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataProtectionServices()
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    ;

builder.Services.AddControllers();

builder.Services.AddSharedServices();
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddHttpClient(BackendApiHttpClientName);
builder.Services.AddDockerControlServices();
builder.Services.AddDockerControllerViewsServices();
builder.Services.AddDockerManagerAuthenticationViewServices();
builder.Services.AddDockerManagerSharedViewServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await app.RunMigrations();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(
        typeof(ServicesExtensions).Assembly,
        typeof(DockerControllerViewsServicesExtensions).Assembly,
        typeof(DockerManagerAuthenticationViewServicesExtensions).Assembly
    )
    .AddInteractiveServerRenderMode()
    ;

app.Run();
