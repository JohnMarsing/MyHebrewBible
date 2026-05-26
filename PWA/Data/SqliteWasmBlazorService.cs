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
				_logger.LogInformation("{Method}, {Message}", nameof(InitializeAsync), "Database already exists in OPFS - skipping download.");
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
}