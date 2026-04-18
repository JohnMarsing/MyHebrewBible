using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Blazored.Toast;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using PWA;
using PWA.Data;
using PWA.Features.Home.Data;
using PWA.Features.Parasha.Data;
using PWA.State;

using Serilog;
using Serilog.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Information()
		.WriteTo.BrowserConsole()
		.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// HttpClient is already registered by default in Blazor WASM

builder.Services.AddHomeData(); 
builder.Services.AddParashaData(); 
builder.Services.AddSingleton<SqliteDataService>();  // ToDo: refactor this...not sure how

builder.Services.AddPWAUpdater(); // This is Toolbelt.Blazor
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredToast();  // Need this here and in the Server

Log.Information("PWA WebAssembly App Starting...");

try
{
	await builder.Build().RunAsync();
	Log.Information("PWA WebAssembly App Stopped cleanly");
}
catch (Exception ex)
{
	Log.Fatal(ex, "PWA WebAssembly App terminated unexpectedly");
	throw;
}
finally
{
	await Log.CloseAndFlushAsync();
}

