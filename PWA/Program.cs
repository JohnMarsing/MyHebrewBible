using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Blazored.LocalStorage;
using Blazored.Toast;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using PWA;
using PWA.Data;
using PWA.HealthChecks.Database.Data;
using PWA.State;

using Serilog;
using PWA.Features.Bible.Data;
using PWA.Features.Bible.TSK.Data;

using SqliteWasmBlazor;  // Added in 026 branch
using PWA.Data.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Information()
		.WriteTo.BrowserConsole()
		.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);
builder.Services.AddSqliteWasm(); // Register SqliteWasmBlazor core services; Added in 026 branch

builder.UseSentry(options =>
{
	options.Dsn = "https://92c51cb6dbb8813a9fc8356a5774910a@o4511417237962752.ingest.us.sentry.io/4511417300025344";
	options.Environment = builder.HostEnvironment.Environment;  // "Development", "Production", etc.
	//options.Debug = builder.HostEnvironment.IsDevelopment();    // Enable debug output in dev
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
builder.Services.AddTSKData();
builder.Services.AddDatabaseHealthChecks();
builder.Services.AddSqliteWasm();
builder.Services.AddSingleton<SqliteWasmBlazorService>();

builder.Services.AddPWAUpdater(); // This is Toolbelt.Blazor
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AppState>();
builder.Services.AddBlazoredToast();  // Need this here and in the Server

Log.Information("PWA WebAssembly App Starting...");

WebAssemblyHost? host = null;
try
{
	host = builder.Build();

	var sqliteWasmBlazorService = host.Services.GetRequiredService<SqliteWasmBlazorService>();
	await sqliteWasmBlazorService.InitializeAsync();

	await host.RunAsync();
	Log.Information("PWA WebAssembly App Stopped cleanly");
}
catch (Exception ex) when (ex.Message.Contains("createSyncAccessHandle") || ex.Message.Contains("Access Handles cannot be created"))
{
	Log.Warning(ex, "PWA WebAssembly App could not start: OPFS database locked by another tab");
	if (host is not null)
	{
		var js = (IJSInProcessRuntime)host.Services.GetRequiredService<IJSRuntime>();
		js.InvokeVoid("eval", """
			(function() {
				var app = document.getElementById('app');
				if (app) {
					app.innerHTML = '<div style="font-family:sans-serif;max-width:540px;margin:80px auto;padding:2rem;border:1px solid #f5c2c7;border-radius:.5rem;background:#fff3cd;text-align:center;">'
						+ '<h2 style="color:#842029;">&#9888; MyHebrewBible is already open</h2>'
						+ '<p style="color:#664d03;font-size:1.1rem;">Another browser tab is already running MyHebrewBible. Only one tab is allowed at a time because of how the local database works.</p>'
						+ '<p><strong>Please close this tab or the other tab, then reload.</strong></p>'
						+ '<button onclick="location.reload()" style="margin-top:1rem;padding:.5rem 1.5rem;font-size:1rem;cursor:pointer;border-radius:.375rem;border:none;background:#0d6efd;color:#fff;">Reload</button>'
						+ '</div>';
				}
			})();
			""");
	}
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

