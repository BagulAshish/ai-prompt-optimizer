using AiPromptOptimizer.Web.Components;
using AiPromptOptimizer.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var baseUrl = config["ApiSettings:BaseUrl"] ?? throw new InvalidOperationException("API base URL is not configured.");

    return new HttpClient
    {
        BaseAddress = new Uri(baseUrl)
    };
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthStateProviderService>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    provider => provider.GetRequiredService<CustomAuthStateProviderService>());

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();