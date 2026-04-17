//using Dapper;
using System.Data;

namespace PWA.Data;

/// <summary>
/// Base repository for Blazor WASM that uses SqliteDataService's pre-initialized connection.
/// Provides centralized error handling and logging for database operations.
/// </summary>
public abstract class BaseRepositoryAsync
{
	private readonly SqliteDataService _dataService;
	protected readonly ILogger Logger;

	protected BaseRepositoryAsync(SqliteDataService dataService, ILogger logger)
	{
		_dataService = dataService;
		Logger = logger;
	}

	/// <summary>
	/// Executes a database query using the shared SQLite connection.
	/// Connection is managed by SqliteDataService (already downloaded to WASM VFS).
	/// </summary>
	protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData, string? sqlForLogging = null)
	{
		try
		{
			var connection = await _dataService.GetConnectionAsync();
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