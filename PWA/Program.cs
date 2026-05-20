using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Blazored.Toast;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using PWA;
using PWA.Data;
using PWA.HealthChecks.Database.Data;
using PWA.State;

using Serilog;
using PWA.Features.Bible.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Information()
		.WriteTo.BrowserConsole()
		.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

builder.UseSentry(options =>
{
	options.Dsn = "https://92c51cb6dbb8813a9fc8356a5774910a@o4511417237962752.ingest.us.sentry.io/4511417300025344";
	options.Environment = builder.HostEnvironment.Environment;  // "Development", "Production", etc.
	options.Debug = builder.HostEnvironment.IsDevelopment();    // Enable debug output in dev
	options.EnableLogs = true;
	options.AutoSessionTracking = true;
	options.TracesSampleRate = 1.0;

	// ToDo: See C:\Source\MyHebrewBibleBackup\002-make-home-to-be-book-chapter\Wiki\ProcessError-Component-Inside-App.md
	//options.AddEventProcessor(new BlazorEventProcessor()); 
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// HttpClient is already registered by default in Blazor WASM

builder.Services.AddBibleData(); 
builder.Services.AddDatabaseHealthChecks(); 
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

