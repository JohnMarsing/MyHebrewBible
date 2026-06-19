namespace PWA.Features.Bible.Data;

public record BookChapterNT
{
	public required BookChapterHeader Header { get; set; }
	public List<WordPartNT>? WordPartList	 { get; init; }
}
