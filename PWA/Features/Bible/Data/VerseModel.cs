namespace PWA.Features.Bible.Data;

public class VerseModel
{
	public required VerseHeader Header { get; set; }	
	public List<WordPart> WordPartList { get; init; } = [];
}