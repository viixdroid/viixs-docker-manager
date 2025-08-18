using DockerManager.Auth.Extensions;
using DockerManager.Components;
using DockerManager.Extensions;
using DockerManager.Providers;
using DockerManager.Services;
using DockerManager.Shared.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using static DockerManager.Constants.ApplicationConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataProtectionServices()
    .AddSharedServices()
    .AddAuthenticationService(builder.Configuration)
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    ;

builder.Services.AddControllers();
builder.Services.AddHttpClient(BackendApiHttpClientName);

//TODO: move to a more appropriate place
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();

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
    .AddInteractiveServerRenderMode()
    ;

app.Run();
