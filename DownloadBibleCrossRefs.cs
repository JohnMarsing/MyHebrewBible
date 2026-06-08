#:package Spectre.Console@*

using Spectre.Console;
/*
=============================================
# Download Bible Cross Reference JSON Files

### Run
dotnet run DownloadBibleCrossRefs.cs
dotnet run --file DownloadBibleCrossRefs.cs
dotnet run --file C:\Source\repos\MyHebrewBible\scripts\DownloadBibleCrossRefs.cs
=============================================
*/

AnsiConsole.Write(new FigletText("Bible CrossRef Downloader").Color(Color.Blue));

var downloadFolder = AnsiConsole.Ask<string>(
		"Enter folder to save the JSON files:",
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "bible-cross-reference-json")
);

if (!Directory.Exists(downloadFolder))
{
	Directory.CreateDirectory(downloadFolder);
	AnsiConsole.MarkupLine($"[green]Created folder:[/] {downloadFolder}");
}

const string baseUrl = "https://raw.githubusercontent.com/josephilipraja/bible-cross-reference-json/master";

using var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(2) };

await AnsiConsole.Progress()
	.Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn(), new RemainingTimeColumn())
		.StartAsync(async ctx =>
		{
			var task = ctx.AddTask("[green]Downloading 32 JSON files[/]", new ProgressTaskSettings { MaxValue = 32 });

			for (int i = 1; i <= 32; i++)
			{
				var fileName = $"{i}.json";
				var url = $"{baseUrl}/{fileName}";
				var filePath = Path.Combine(downloadFolder, fileName);

				task.Description = $"Downloading {fileName}";

				try
				{
					var response = await httpClient.GetAsync(url);
					response.EnsureSuccessStatusCode();

					await using var fs = File.Create(filePath);
					await response.Content.CopyToAsync(fs);

					task.Increment(1);
				}
				catch (Exception ex)
				{
					AnsiConsole.MarkupLine($"[red]Failed[/] {fileName}: {ex.Message}");
					task.Increment(1);
				}
			}
		});

AnsiConsole.MarkupLine($"[green]✅ Download complete![/] Files saved to: [cyan]{downloadFolder}[/]");
AnsiConsole.MarkupLine("[grey]You can now run your importer script.[/]");