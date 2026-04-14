using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PWA;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using PWA.Data;

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

builder.Services.AddSingleton<SqliteDataService>();
builder.Services.AddPWAUpdater(); // This is Toolbelt.Blazor

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

