namespace PWA.Features.Bible.Data;

public record BookChapterWithOT
{
	public required BookChapterHeader Header { get; set; }
	public List<WordPart>? WordPartList { get; init; }
}
