namespace PWA.Features.Bible.Data;

public class ReportModel
{
	public int ID { get; init; }
	public string? BCV { get; init; }
	public int Verse { get; init; }
	public string? VerseOffset { get; init; }
	public string? KJV { get; init; }
	public string? DescH { get; init; }
	public string? DescD { get; init; }
	public int BookID { get; init; }
	public int Chapter { get; init; }
	//public int LastVerse { get; init; } // This shouldn't be here
	public List<WordPart> WordPartList { get; init; } = new();
}

// Ignore Spelling: BCV, Strongs, bigint