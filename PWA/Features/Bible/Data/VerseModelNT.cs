namespace PWA.Features.Bible.Data;

public class VerseModelNT
{
	public required VerseHeader Header { get; set; }
	public List<WordPartNT> WordPartList { get; init; } = [];
}

// Ignore Spelling: BCV, Strongs, bigint