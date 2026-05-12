using Blazored.LocalStorage;

//using PWA.Features.Haggadah;
//using PWA.State.BookChapter;
//using BookChapterState = PWA.State.BookChapter;

namespace PWA.State;

public class AppState 
{
	public Features.Bible.State? BookChapterState { get; }
	//public Features.Haggadah.State? HaggadahState { get; }	
	//public VerseList.VerseListState? VerseListState { get; }

	#region Constructor and DI
	private readonly ILogger? Logger;
	private readonly ILocalStorageService?  localStorage; 

	public AppState(ILocalStorageService localStorage, ILogger<AppState> logger) 
	{
		Logger = logger;
		this.localStorage = localStorage;
		BookChapterState = new Features.Bible.State(localStorage, logger);
		//HaggadahState = new Features.Haggadah.State(localStorage, logger);	
		//VerseListState = new VerseList.VerseListState(localStorage, logger);
		//Logger!.LogInformation("ctor of {Project}!{Class}", nameof(PWA), nameof(AppState));
	}
	#endregion

	private bool _isInitialized;

	public async Task Initialize()
	{
		//Logger!.LogInformation("{Method}, _isInitialized: {_isInitialized}", nameof(Initialize), _isInitialized);
		if (!_isInitialized)
		{
			try
			{
				await BookChapterState!.Initialize();
				//await HaggadahState!.Initialize();	
				//await VerseListState!.Initialize();
				//Logger!.LogWarning("{Method} ParashaState.Get: {ParashaState}", nameof(Initialize), ParashaState.Get());
				_isInitialized = true;
			}
			catch (Exception ex) 
			{
				Logger!.LogError(ex, "{Class}!{Method}", nameof(AppState), nameof(Initialize));
			}

		}
	}
}

// Ignore Spelling: App, Abrv