using System.Data;

namespace PWA.Data;

/// <summary>
/// Base repository for Blazor WASM SqliteWasmBlazorService
/// Provides centralized error handling and logging for database operations.
/// </summary>
public abstract class BaseRepositoryAsync
{
	private readonly SqliteWasmBlazorService _sqliteService;

	protected readonly ILogger Logger;

	protected BaseRepositoryAsync(SqliteWasmBlazorService sqliteService, ILogger logger)
	{
		_sqliteService = sqliteService; ; 
		Logger = logger;
	}

	/// <summary>
	/// Executes a database query using the shared SQLite connection.
	/// Connection is managed by SqliteWasmBlazorService (already downloaded to WASM VFS).
	/// </summary>
	protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData, string? sqlForLogging = null)
	{
		try
		{
			await using var connection = await _sqliteService.CreateOpenConnectionAsync();
			return await getData(connection);
		}
		catch (Exception ex)
		{
			var methodName = $"{GetType().Name}.{nameof(WithConnectionAsync)}";
			Logger.LogError(ex, "{Method} failed. SQL: {Sql}", methodName, sqlForLogging ?? "N/A");
			throw; // Re-throw original exception
		}
	}

}