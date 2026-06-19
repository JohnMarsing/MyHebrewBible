using Dapper;
using PWA.Data.Constants;
using SqliteWasmBlazor;

namespace PWA.Data;

public sealed class SqliteWasmBlazorService
{
	private readonly IServiceProvider _services;
	private readonly HttpClient _httpClient;
	private readonly ISqliteWasmDatabaseService _databaseService;
	private readonly ILogger<SqliteWasmBlazorService> _logger;
	private readonly SemaphoreSlim _initLock = new(1, 1);

	private bool _initialized;

	public SqliteWasmBlazorService(
		IServiceProvider services,
		HttpClient httpClient,
		ISqliteWasmDatabaseService databaseService,
		ILogger<SqliteWasmBlazorService> logger)
	{
		_services = services;
		_httpClient = httpClient;
		_databaseService = databaseService;
		_logger = logger;
	}

	public async Task InitializeAsync()
	{
		if (_initialized)
		{
			return;
		}

		await _initLock.WaitAsync();
		try
		{
			if (_initialized)
			{
				return;
			}

			await _services.InitializeSqliteWasmAsync();

			const string dbName = Database.DbFileName;

			if (!await _databaseService.ExistsDatabaseAsync(dbName))
			{
				var bytes = await _httpClient.GetByteArrayAsync(dbName);
				await _databaseService.ImportDatabaseAsync(dbName, bytes);
				_logger.LogInformation("{Method}, {Message}", nameof(InitializeAsync), "Initial database imported successfully");
			}
			else
			{
				int localVersion = await GetUserVersionAsync(dbName);
				if (localVersion < Database.CurrentSchemaVersion)
				{
					_logger.LogInformation("{Method}, {Message}, LocalVersion={Local}, Required={Required}",
						nameof(InitializeAsync),
						"New table(s) or schema change detected - deleting and re-importing database to reflect changes",
						localVersion, Database.CurrentSchemaVersion);

					await _databaseService.DeleteDatabaseAsync(dbName);

					var bytes = await _httpClient.GetByteArrayAsync(dbName);
					await _databaseService.ImportDatabaseAsync(dbName, bytes);

					_logger.LogInformation("{Method}, {Message}", nameof(InitializeAsync), "Database re-imported successfully after schema update");
				}
				else
				{
					_logger.LogInformation("{Method}, {Message}, Version={Version}",
						nameof(InitializeAsync), "Database already exists in OPFS - skipping download.", localVersion);
				}
			}

			_initialized = true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "{Method}, {Message}", nameof(InitializeAsync), "Failed to initialize SqliteWasmBlazor.");
			throw;
		}
		finally
		{
			_initLock.Release();
		}
	}

	public async Task<SqliteWasmConnection> CreateOpenConnectionAsync()
	{
		await InitializeAsync();

		var connection = new SqliteWasmConnection($"Data Source={Database.DbFileName}");
		await connection.OpenAsync();
		return connection;
	}

	private async Task<int> GetUserVersionAsync(string dbName)
	{
		await using var connection = new SqliteWasmConnection($"Data Source={dbName}");
		await connection.OpenAsync();
		try
		{
			// PRAGMA user_version returns an integer (0 for brand-new or never-set DBs)
			var version = await connection.QueryFirstOrDefaultAsync<int>("PRAGMA user_version;");
			return version;
		}
		finally
		{
			await connection.CloseAsync();
		}
	}
}