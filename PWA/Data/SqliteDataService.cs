using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Data;

namespace PWA.Data;

public class SqliteDataService : IAsyncDisposable
{
	private readonly HttpClient _http;
	private readonly ILogger<SqliteDataService> _logger;
	private SqliteConnection? _connection;
	private readonly SemaphoreSlim _initLock = new(1, 1);
	private bool _initialized = false;

	public SqliteDataService(HttpClient http, ILogger<SqliteDataService> logger)
	{
		_http = http;
		_logger = logger;
	}

	// Called by: DatabaseSmokeTest.razor (Features/Home/)
	public async Task<long> GetScriptureCountAsync()
	{
		await EnsureInitializedAsync();

		const string sql = "SELECT COUNT(*) AS RowCnt FROM Scripture";
		_logger.LogInformation("Executing scripture count query.");

		try
		{
			var count = await _connection!.ExecuteScalarAsync<long>(sql);
			_logger.LogInformation("Scripture count query completed. Count: {Count}", count);
			return count;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error executing scripture count query.");
			throw;
		}
	}

	private async Task EnsureInitializedAsync()
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

			_logger.LogInformation("Initializing SQLite data service.");

			var dbFileName = "MhbSqlite.db";
			var dbPath = "/" + dbFileName;

			_logger.LogInformation("Downloading SQLite database file: {DatabaseFile}", dbFileName);
			var dbBytes = await _http.GetByteArrayAsync(dbFileName);

			_logger.LogInformation("Writing SQLite database to WASM virtual file system: {DatabaseFile}", dbFileName);
			await WriteDbToVirtualFsAsync(dbBytes, dbFileName);

			await VerifyDbFileAsync(dbPath, dbBytes.Length);

			var connectionString = $"Data Source={dbPath};Mode=ReadOnly;Cache=Shared";
			_logger.LogInformation("Opening SQLite connection. ConnectionString: {ConnectionString}", connectionString);

			_connection = new SqliteConnection(connectionString);
			await _connection.OpenAsync();

			_initialized = true;
			_logger.LogInformation("SQLite data service initialized successfully.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to initialize SQLite data service.");
			throw;
		}
		finally
		{
			_initLock.Release();
		}
	}

	private async Task VerifyDbFileAsync(string path, int expectedLength)
	{
		if (!File.Exists(path))
		{
			throw new FileNotFoundException($"SQLite database file was not found after write: {path}", path);
		}

		var fileInfo = new FileInfo(path);
		if (fileInfo.Length != expectedLength)
		{
			throw new InvalidOperationException(
				$"SQLite database file length mismatch. Expected {expectedLength} bytes, actual {fileInfo.Length} bytes.");
		}

		var header = new byte[16];
		await using var stream = File.OpenRead(path);
		var bytesRead = await stream.ReadAsync(header);

		if (bytesRead < 16)
		{
			throw new InvalidOperationException("SQLite database file is too small to contain a valid header.");
		}

		var headerText = System.Text.Encoding.ASCII.GetString(header);
		if (!headerText.StartsWith("SQLite format 3"))
		{
			throw new InvalidOperationException(
				$"SQLite database header is invalid. Header read: '{headerText}'.");
		}

		_logger.LogInformation(
			"Verified SQLite DB file at {Path}. Size={Length} bytes, Header='{Header}'",
			path,
			fileInfo.Length,
			headerText);
	}
	
	private async Task WriteDbToVirtualFsAsync(byte[] bytes, string fileName)
	{
		var path = "/" + fileName;
		await File.WriteAllBytesAsync(path, bytes);
		_logger.LogInformation("Database file written to virtual FS at: {Path}", path);
	}

	public async Task<SqliteConnection> GetConnectionAsync()
	{
		await EnsureInitializedAsync();
		return _connection!;
	}

	public async ValueTask DisposeAsync()
	{
		if (_connection != null)
		{
			_logger.LogInformation("Disposing SQLite connection.");
			await _connection.DisposeAsync();
			_connection = null;
		}

		_initLock.Dispose();
		_logger.LogInformation("SqliteDataService disposed.");
	}
}