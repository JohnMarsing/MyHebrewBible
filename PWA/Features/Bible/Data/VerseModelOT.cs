namespace PWA.Features.Bible.Data;

public class VerseModelOT
{
	public required VerseHeader Header { get; set; }	
	public List<WordPart> WordPartList { get; init; } = [];
}