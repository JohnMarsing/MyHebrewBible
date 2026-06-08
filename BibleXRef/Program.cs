# : package Dapper
# : package Microsoft.Data.SqlClient
# : package Spectre.Console
# : package System.Text.Json

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using System.Text.Json;

// =============================================
// Bible Cross Reference JSON Importer (File-based)
// Run with: dotnet run BibleXRef.cs
// =============================================

const string ConnectionString = "Server=(local);Database=OpenGNT_version3_3;Trusted_Connection=True;TrustServerCertificate=True;";
const string TableName = "XRef";

AnsiConsole.Write(new FigletText("Bible CrossRef Importer").Color(Color.Blue));

var jsonFolder = AnsiConsole.Ask<string>("Enter folder path containing the 32 JSON files:", @"C:\Data\bible-xref-json");

if (!Directory.Exists(jsonFolder))
{
	AnsiConsole.MarkupLine("[red]Folder not found![/]");
	return;
}

await CreateTableIfNotExists();

var files = Enumerable.Range(1, 32)
											.Select(i => Path.Combine(jsonFolder, $"{i}.json"))
											.Where(File.Exists)
											.ToList();

AnsiConsole.MarkupLine($"Found {files.Count} JSON files.");

await ImportAllFiles(files);

AnsiConsole.MarkupLine("[green]Import completed successfully![/]");

async Task CreateTableIfNotExists()
{
	using var connection = new SqlConnection(ConnectionString);
	await connection.OpenAsync();

	var createSql = $@"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{TableName}')
BEGIN
    CREATE TABLE [{TableName}] (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        VerseId INT NOT NULL,
        VerseRef NVARCHAR(20) NOT NULL,
        CrossRefVerseId INT NOT NULL,
        CrossRefVerse NVARCHAR(20) NOT NULL,
        CreatedAt DATETIME2 DEFAULT GETUTCDATE()
    );

    CREATE UNIQUE INDEX IX_CrossRef ON [{TableName}] (VerseId, CrossRefVerseId);
END";
	await connection.ExecuteAsync(createSql);
}

async Task ImportAllFiles(List<string> files)
{
	using var connection = new SqlConnection(ConnectionString);
	await connection.OpenAsync();

	await AnsiConsole.Progress()
			.StartAsync(async ctx =>
			{
				var task = ctx.AddTask("[green]Importing cross references...[/]", new ProgressTaskSettings { MaxValue = files.Count });

				foreach (var file in files)
				{
					var fileName = Path.GetFileName(file);
					task.Description = $"Processing {fileName}";

					var json = await File.ReadAllTextAsync(file);
					var data = JsonSerializer.Deserialize<Dictionary<string, VerseData>>(json);

					if (data == null)
					{
						task.Increment(1);
						continue;
					}

					var batch = new List<CrossRefRecord>();

					foreach (var kvp in data)
					{
						int verseId = int.Parse(kvp.Key);
						var verse = kvp.Value;

						foreach (var refKvp in verse.r)
						{
							batch.Add(new CrossRefRecord
							{
								VerseId = verseId,
								VerseRef = verse.v,
								CrossRefVerseId = int.Parse(refKvp.Key),
								CrossRefVerse = refKvp.Value
							});
						}
					}

					const string insertSql = $@"
INSERT INTO [{TableName}] (VerseId, VerseRef, CrossRefVerseId, CrossRefVerse)
VALUES (@VerseId, @VerseRef, @CrossRefVerseId, @CrossRefVerse)";

					await connection.ExecuteAsync(insertSql, batch);

					task.Increment(1);
				}
			});
}

public class VerseData
{
	public string v { get; set; } = string.Empty;
	public Dictionary<string, string> r { get; set; } = new();
}

public class CrossRefRecord
{
	public int VerseId { get; set; }
	public string VerseRef { get; set; } = string.Empty;
	public int CrossRefVerseId { get; set; }
	public string CrossRefVerse { get; set; } = string.Empty;
}