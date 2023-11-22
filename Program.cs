using BlazorServerAppConfigTutorial.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Azure.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Initialize ConfigurationManager with the standard providers (appsettings.json, environment variables, etc)
var configuration = builder.Configuration;
// Note: Key Vault may actually be included for any environment! (Production, Development, Staging, etc.)
// You can actually use Key Vault in Dev (running in Visual Studio) by authenticating in Visual Studio!
// Reference: https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.4.1/index.html
// To authenticate in Visual Studio select the Tools > Options menu to launch the Options dialog.
// Then navigate to the Azure Service Authentication options to sign in with your Azure Active Directory account.
if (builder.Environment.IsProduction())
{
    configuration.AddAzureKeyVault(new Uri("https://kentestkv.vault.azure.net/"), new DefaultAzureCredential());
}
// Quick test in debug (also see <h2>@Configuration["MySecret"]</h2> in index.razor)
// Console.WriteLine(configuration["MySecret"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
